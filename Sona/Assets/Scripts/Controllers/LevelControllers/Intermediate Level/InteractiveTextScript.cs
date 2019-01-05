using ApplicationConstants;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class InteractiveTextScript : MonoBehaviour {

    float textSize = 0.05f;

    TextMeshPro _text;
    GameController gameController;
    Camera playerCam;

    void Start () {
        gameController = FindObjectOfType<GameController>();
        _text = GetComponent<TextMeshPro>();
        _text.enabled = false;
	}
	
	void Update () {
		
	}

    public void ShowText() {
        _text.enabled = true;
        playerCam = gameController.GetActivePlayer().gameObject.GetComponentInChildren<Camera>();
        Transform cam = playerCam.transform;
        var TextPos = cam.position + cam.forward;
        _text.transform.position = TextPos;
        _text.transform.localScale = Vector3.one * textSize;
        _text.transform.rotation = cam.rotation;
    }

    public void HideText() {
        _text.enabled = false;
    }
}
