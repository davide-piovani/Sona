using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InsertCombinationDigit : MonoBehaviour {

    Text _text;
    
    void Awake()
    {
        _text = GetComponent<Text>();
        _text.text = "";
    }

    public void AddDigit(string digit)
    {
        _text.text += digit;
    }

    public void DeleteDigit()
    {
        if (_text.text.Equals("")) return;
        string newText = "";
        for (int i = 0; i < _text.text.Length - 1; i++) { newText += _text.text[i]; }
        _text.text = newText;
    }

    public string GetText() {
        return _text.text;
    }

    public int GetTextLenght() { return _text.text.Length; }

    public void SetText(string digit) {
        _text.text = digit;
    }

}
