using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractiveTextScript : MonoBehaviour {

    TextMeshPro _text;

    void Start () {
        _text = GetComponent<TextMeshPro>();
        _text.enabled = false;
	}
	
	void Update () {
		
	}

    public void ShowText() {
        _text.enabled = true;
        Transform cam = Camera.main.transform;
        var TextPos = cam.position + cam.forward;
        _text.transform.position = TextPos;
        _text.transform.localScale = Vector3.one * 0.05f /*0.025f*/;
        _text.transform.rotation = cam.rotation;
    }

    public void HideText() {
        _text.enabled = false;
    }
}
