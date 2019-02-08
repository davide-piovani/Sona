using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityStandardAssets.CrossPlatformInput;
using ApplicationConstants;

public class MenuMovementController : InputListener {

    [SerializeField] protected TextMeshProUGUI[] buttons;
    protected bool buttonChanged;
    protected int currentButton = 0;

    // Use this for initialization
    public void InitializeMenu () {
        SelectButton(0, true);
        for (int i = 1; i < buttons.Length; i++) SelectButton(i, false);
    }

    public void CheckVerticalInput(){
        float vertical = GameSettings.GetMenuAxis(PlayersConstants.y_Axis);

        if (vertical > 0){
            if (!buttonChanged) ChangeSelectedButton(false);
        } else if (vertical < 0) {
            if (!buttonChanged) ChangeSelectedButton(true);
        } else {
            buttonChanged = false;
        }
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

    public void SelectButton(int buttonIndex, bool selected){
        if (selected){
            buttons[buttonIndex].color = MenuConstants.selectedButtonColor;
            currentButton = buttonIndex;
        } else {
            buttons[buttonIndex].color = MenuConstants.unselectedButtonColor;
        }
    }
}
