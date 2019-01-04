using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyScript : MonoBehaviour {

    public GameObject _door;

    DisplayActivator _display;
    LockedDoor keyDoorController;
    
    void Start() {
        _display = GetComponent<DisplayActivator>();
        keyDoorController = _door.GetComponentInChildren<LockedDoor>();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.B) & _display.IsActive())
        {
            keyDoorController.KeyTaken(_display.GetPlayer().name);
            gameObject.SetActive(false);
        }
    }

}
