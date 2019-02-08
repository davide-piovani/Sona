using ApplicationConstants;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InsertNameController : InputListener {

    public KeyboardController _keyboard;
    public TextMeshProUGUI _cancel;
    public TextMeshProUGUI _ok;
    Action<string> callback;
    bool buttonChanged = false;
    int currentButton = 0;
    string _name = "";

    void Start () {
        DisableInput();
        _keyboard.ActiveInput();
	}

    public void SetAllert(string cancelMessage, string okMessage, Action<string> callbackFunc)
    {
        _cancel.text = cancelMessage;
        _ok.text = okMessage;
        callback = callbackFunc;
        gameObject.SetActive(true);
    }

    public void SetDimensions(Canvas canvas)
    {
        transform.SetParent(canvas.transform);
        RectTransform rectTransform = GetComponent<RectTransform>();

        rectTransform.anchoredPosition = new Vector2(0f, 0f);
        rectTransform.pivot = new Vector2(0.5f, 0.5f);
        rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, GameConstants.alertWidth);
        rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, GameConstants.alertWidth);
        rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, GameConstants.alertHeight);
        rectTransform.localScale = new Vector3(1f, 1f, 1f);
    }

    void Update () {
        if (IsInputActive()) {
            CheckSelection();
            CheckEnterButton();
        }
	}

    public void NameChosen(string name) {
        _name = name;
        _keyboard.DisableInput();
        ActiveInput();
        SelectButton(1);
    }

    private void SelectButton(int index)
    {
        if (index == 0)
        {
            _cancel.color = MenuConstants.selectedButtonColor;
            _ok.color = MenuConstants.unselectedButtonColor;
            currentButton = index;
        }
        if (index == 1)
        {
            _cancel.color = MenuConstants.unselectedButtonColor;
            _ok.color = MenuConstants.selectedButtonColor;
            currentButton = index;
        }
    }

    private void CheckSelection()
    {
        float horizontal = GameSettings.GetMenuAxis(PlayersConstants.x_Axis);

        if (horizontal > 0 || horizontal < 0) { if (!buttonChanged) NextButton(); }
        else { buttonChanged = false; }
    }

    private void NextButton()
    {
        buttonChanged = true;
        if (currentButton == 0) currentButton++;
        else currentButton = 0;

        SelectButton(currentButton);
    }

    private void CheckEnterButton()
    {
        if (GameSettings.GetButtonDown(PlayersConstants.enterButton))
        {
            RestoreOldListener();
            if (currentButton == 1) callback(_name);
            Destroy(gameObject);
        }
    }

}
