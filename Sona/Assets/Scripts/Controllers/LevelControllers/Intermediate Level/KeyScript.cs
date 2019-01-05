using ApplicationConstants;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class KeyScript : MonoBehaviour {

    public GameObject _door;

    DisplayActivator _display;
    LockedDoor keyDoorController;
    
    void Start() {
        _display = GetComponent<DisplayActivator>();
        keyDoorController = _door.GetComponentInChildren<LockedDoor>();
    }

    void Update() {
        if (CrossPlatformInputManager.GetButtonDown(PlayersConstants.interactButton)/*Input.GetKeyDown(KeyCode.B)*/ & _display.IsActive())
        {
            keyDoorController.KeyTaken(_display.GetPlayer().name);
            gameObject.SetActive(false);
        }
    }

}
