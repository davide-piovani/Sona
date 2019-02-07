using ApplicationConstants;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScrollbarController : SettingsElement {

    string controllerType;
    [SerializeField] TextMeshProUGUI controllerText;
    [SerializeField] TextMeshProUGUI leftArrow;
    [SerializeField] TextMeshProUGUI typeText;
    [SerializeField] TextMeshProUGUI rightArrow;

    public override void Select(bool selected)
    {
        if (selected)
        {
            controllerText.color = MenuConstants.selectedButtonColor;
            leftArrow.color = MenuConstants.selectedButtonColor;
            typeText.color = MenuConstants.selectedButtonColor;
            rightArrow.color = MenuConstants.selectedButtonColor;
        }
        else
        {
            controllerText.color = MenuConstants.unselectedButtonColor;
            leftArrow.color = MenuConstants.unselectedButtonColor;
            typeText.color = MenuConstants.unselectedButtonColor;
            rightArrow.color = MenuConstants.unselectedButtonColor;
        }
    }

    public string GetControllerType() {
        return typeText.text;
    }

    public void SetControllerType(string txt) {
        typeText.text = txt;
    }

    public void ChangeScrollbarValue(bool cond)
    {
        if (cond)
        {
            if (typeText.text.ToString().Equals(GameConstants.keyboardPad)) { typeText.SetText(GameConstants.xboxPad); }
            else if (typeText.text.ToString().Equals(GameConstants.xboxPad)) { typeText.SetText(GameConstants.playStationPad); }
            else { typeText.SetText(GameConstants.keyboardPad); }
        }
        else {
            if (typeText.text.ToString().Equals(GameConstants.keyboardPad)) { typeText.SetText(GameConstants.playStationPad); }
            else if (typeText.text.ToString().Equals(GameConstants.xboxPad)) { typeText.SetText(GameConstants.keyboardPad); }
            else { typeText.SetText(GameConstants.xboxPad); }
        }
    }
    
}
