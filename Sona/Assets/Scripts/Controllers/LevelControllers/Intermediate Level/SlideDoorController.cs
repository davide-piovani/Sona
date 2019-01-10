using ApplicationConstants;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class SlideDoorController : MonoBehaviour {

    public InteractController _interact;
    ElevatorDoor _door;

    void Start()
    {
        _door = GetComponent<ElevatorDoor>();
    }

    void Update()
    {

        if (CrossPlatformInputManager.GetButtonDown(PlayersConstants.interactButton) & _interact.IsActive() & !_door.IsSliding())
        {
            _door.SlideDoor();
        }

    }
}
