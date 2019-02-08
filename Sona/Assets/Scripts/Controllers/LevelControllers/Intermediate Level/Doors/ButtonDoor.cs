using ApplicationConstants;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class ButtonDoor : MonoBehaviour {

    public InteractController _buttonInteractController;
    public Camera _camera;
    public GameObject led;
    public Material locked;
    public Material unlocked;
    
    bool active = false;
    
    void Start () {
        _camera.enabled = false;
        led.GetComponent<Renderer>().material = locked;
    }
	
	void Update () {

        if (_buttonInteractController.IsActive() & GameSettings.GetButtonDown(PlayersConstants.interactButton)) {
            _buttonInteractController.GetPlayer().GetComponentInChildren<Camera>().enabled = false;
            _buttonInteractController.Necessary(false);
            _camera.enabled = true;
            Invoke("UnlockDoor", 1f);
            Invoke("EndCutScene", 3f);
        }

    }

    void UnlockDoor() {
        led.GetComponent<Renderer>().material = unlocked;
    }

    void EndCutScene() {
        active = true;
        _camera.enabled = false;
        _buttonInteractController.GetPlayer().GetComponentInChildren<Camera>().enabled = true;
        _buttonInteractController.ForceDeActive();
    }

    public bool isUnlocked() { return active; }
}
