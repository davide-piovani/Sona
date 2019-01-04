using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractiveTextScript : MonoBehaviour {

    public float textSize = 0.05f;

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
        _text.transform.localScale = new Vector3(textSize, 0.05f, 0.05f);/*Vector3.one * textSize;*/ /*0.025f*/;
        _text.transform.rotation = cam.rotation;
    }

    public void HideText() {
        _text.enabled = false;
    }
}
