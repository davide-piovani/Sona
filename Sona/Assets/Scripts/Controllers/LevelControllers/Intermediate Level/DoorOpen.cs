using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour {

    public KeyDoorController _door;
    TutorialManager _tutorial;

    void Start() {
        _tutorial = FindObjectOfType<TutorialManager>();
    }

    void Update () {
        if (_door.isKeyUsed()) {
            _tutorial.DoorOpen();
            gameObject.SetActive(false);
        }
	}
}
