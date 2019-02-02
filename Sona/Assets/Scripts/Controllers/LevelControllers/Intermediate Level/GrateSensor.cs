using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrateSensor : MonoBehaviour {

    AllarmButton _allarm;

    void Start () {
        _allarm = FindObjectOfType<AllarmButton>();
	}

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Guard")) {
            _allarm.GuardPassed();
        }
    }
}
