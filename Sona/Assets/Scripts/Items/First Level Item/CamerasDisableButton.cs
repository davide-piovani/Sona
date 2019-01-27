using ApplicationConstants;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class CamerasDisableButton : MonoBehaviour {

    InteractController _interactController;
    Allarm allarm;

	void Start () {
        _interactController = GetComponent<InteractController>();
        allarm = FindObjectOfType<Allarm>();
    }

    void Update() {

        if (_interactController.IsActive() & CrossPlatformInputManager.GetButtonDown(PlayersConstants.interactButton)) {
            _interactController.ForceDeActive();
            allarm.deactiveAllarm();
        }
    }
}
