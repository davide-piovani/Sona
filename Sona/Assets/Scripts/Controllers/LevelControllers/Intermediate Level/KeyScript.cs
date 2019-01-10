using ApplicationConstants;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class KeyScript : MonoBehaviour {

    public GameObject _door;

    InteractController _interact;
    LockedDoor keyDoorController;
    
    void Start() {
        _interact = GetComponent<InteractController>();
        keyDoorController = _door.GetComponentInChildren<LockedDoor>();
    }

    void Update() {
        if (CrossPlatformInputManager.GetButtonDown(PlayersConstants.interactButton) & _interact.IsActive())
        {
            keyDoorController.KeyTaken(_interact.GetPlayer().name);
            _interact.Necessary(false);
            _interact.ForceDeActive();
            gameObject.SetActive(false);
        }
    }

}
