using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractiveText : MonoBehaviour {

    Text _text;
    Image pressImage;
    bool active = false;

    KeyButtonScript _keyButton;

    void Start() {
        _keyButton = FindObjectOfType<KeyButtonScript>();
        pressImage = GetComponentInChildren<Image>();
        _text = gameObject.GetComponent<Text>();
        pressImage.color = new Vector4(pressImage.color.r, pressImage.color.g, pressImage.color.b, 0);
        _text.text = "";
    }

    void Update() {
       _text.enabled = active;
    }

    public bool OwnText(bool press) {
        if (!active) {
            if (press) { _text.text = "Press"; }
            else { _text.text = "Hold "; }
            pressImage.color = new Vector4(pressImage.color.r, pressImage.color.g, pressImage.color.b, 1);
            pressImage.sprite = _keyButton.GetInteractButtonImage();
            active = true;
            return true;
        }
        else {
            return false;
        }
    }

    public bool DeOwnText() {
        if (active) {
            _text.text = "";
            pressImage.color = new Vector4(pressImage.color.r, pressImage.color.g, pressImage.color.b, 0);
            pressImage.sprite = null;
            active = false;
            return true;
        }
        else {
            return false;
        }
    }

    public void SetText(bool press) {
        if (press) { _text.text = "Press"; }
        else { _text.text = "Hold "; }
        pressImage.color = new Vector4(pressImage.color.r, pressImage.color.g, pressImage.color.b, 1);
        pressImage.sprite = _keyButton.GetInteractButtonImage();
    }

}
