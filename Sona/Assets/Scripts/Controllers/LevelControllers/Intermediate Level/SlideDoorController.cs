using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideDoorController : MonoBehaviour {

    public DisplayActivator _display;
    ElevatorDoor _door;

    void Start()
    {
        _door = GetComponent<ElevatorDoor>();
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.B) & _display.IsActive() & !_door.IsSliding())
        {
            _door.SlideDoor();
        }

    }
}
