using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractiveText : MonoBehaviour {

    Text _text;
    bool active = false;
    
    void Start() {
        _text = gameObject.GetComponent<Text>();
    }

    void Update() {
       _text.enabled = active;
    }

    public bool OwnText(string text) {
        if (!active) {
            _text.text = text;
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
            active = false;
            return true;
        }
        else {
            return false;
        }
    }

    public void SetText(string txt) {
        _text.text = txt;
    }

}
