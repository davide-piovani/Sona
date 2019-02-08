using ApplicationConstants;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KeyboardButton : MonoBehaviour {

    Image _button;
    TextMeshProUGUI _text;
    InsertString _string;
    KeyboardController _controller;

    void Awake() {
        _button = GetComponent<Image>();
        _text = GetComponentInChildren<TextMeshProUGUI>();
        _string = FindObjectOfType<InsertString>();
        _controller = GetComponentInParent<KeyboardController>();
    }

    public void Selected(bool selected) {
        if (selected) {
            _button.color = MenuConstants.selectedButtonColor;
            _text.color = MenuConstants.selectedButtonColor;
        }
        else
        {
            _button.color = MenuConstants.unselectedButtonColor;
            _text.color = MenuConstants.unselectedButtonColor;
        }
    }

    public void ClickButton() {
        if (_text.text.Equals("End")) {
            if (_string.GetTextLenght() > 0) _controller.EndStringInsert(_string.GetText());
            else { }
        }
        else if (_text.text.Equals("Canc")) { _string.DeleteDigit(); }
        else { if(_string.GetTextLenght() < 10) _string.AddDigit(_text.text); }
    }

}
