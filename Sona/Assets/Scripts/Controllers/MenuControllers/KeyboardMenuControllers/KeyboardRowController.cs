using ApplicationConstants;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardRowController : InputListener {

    [SerializeField] KeyboardButton[] buttons;
    bool buttonChanged = false;
    int currentButton = 0;


    void Update() {
        if (IsInputActive())
        {
            CheckHorizontalInput();
            CheckEnterButton();
        }
    }

    public void ActiveRow(int buttonIndex) {
        ActiveInput();
        currentButton = buttonIndex;
        for (int i = 0; i < buttons.Length; i++) { buttons[i].Selected(false); }
        SelectButton(currentButton, true);
    }

    public void DeactiveRow() {
        DisableInput();
        SelectButton(currentButton, false);
    }

    public void SelectButton(int buttonIndex, bool cond) {
        if (cond) { currentButton = buttonIndex;  }
        buttons[buttonIndex].Selected(cond);
    }

    public int GetCurrentButton() {
        return currentButton;
    }

    public void SetCurrentButton(int val) {
        currentButton = val;
    }

    public void CheckEnterButton() {
        if (GameSettings.GetButtonDown(PlayersConstants.enterButton)) { buttons[currentButton].ClickButton(); }
    }

    public void CheckHorizontalInput() {
        float horizontal = GameSettings.GetMenuAxis(PlayersConstants.x_Axis);

        if (horizontal < 0) { if (!buttonChanged) ChangeSelectedButton(false); }
        else if (horizontal > 0) { if (!buttonChanged) ChangeSelectedButton(true); }
        else { buttonChanged = false; }
    }

    public void ChangeSelectedButton(bool nextButton) {
        buttonChanged = true;
        SelectButton(currentButton, false);
        SelectButton(nextButton ? NextButton() : PreviousButton(), true);
    }

    private int NextButton() {
        int i = currentButton;

        while (i != buttons.Length - 1) { if (buttons[++i].enabled) return i; }
        i = 0;

        while (!buttons[i].enabled) i++;
        return i;
    }

    private int PreviousButton() {
        int i = currentButton;

        while (i != 0) { if (buttons[--i].enabled) return i; }
        i = buttons.Length - 1;

        while (!buttons[i].enabled) i--;
        return i;
    }

}
