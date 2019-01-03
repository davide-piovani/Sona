using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyScript : MonoBehaviour {

    DisplayActivator _display;
    public KeyDoorController keyDoorController;

    void Start() {
        _display = GetComponent<DisplayActivator>();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.B) & _display.IsActive())
        {
            keyDoorController.KeyTaken(_display.GetPlayer().name);
            gameObject.SetActive(false);
        }
    }

}
