using ApplicationConstants;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombinationButton : MonoBehaviour {

    Image _button;
    Text _text;
    InsertCombinationDigit _string;
    ButtonsController _controller;

    void Awake() {
        _button = GetComponent<Image>();
        _text = GetComponentInChildren<Text>();
        _string = FindObjectOfType<InsertCombinationDigit>();
        _controller = GetComponentInParent<ButtonsController>();
    }

    public void Selected(bool selected)
    {
        if (selected)
        {
            _button.color = MenuConstants.selectedButtonColor;
        }
        else
        {
            _button.color = MenuConstants.unselectedButtonColor;
        }
    }

    public void ClickButton() {
        _string.AddDigit(_text.text);
    }
}
