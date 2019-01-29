using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDoorSensor : MonoBehaviour {

    TutorialManager _tutorial;

    void Start() {
        _tutorial = FindObjectOfType<TutorialManager>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") & other.gameObject.name.Equals("Charlie")) {
            _tutorial.DoorPassed();
            gameObject.SetActive(false);
        }
        
    }

}
