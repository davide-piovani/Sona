using ApplicationConstants;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class SlideDoorController : MonoBehaviour {

    public DisplayActivator _display;
    ElevatorDoor _door;

    void Start()
    {
        _door = GetComponent<ElevatorDoor>();
    }

    void Update()
    {

        if (CrossPlatformInputManager.GetButtonDown(PlayersConstants.interactButton)/*Input.GetKeyDown(KeyCode.B)*/ & _display.IsActive() & !_door.IsSliding())
        {
            _door.SlideDoor();
        }

    }
}
