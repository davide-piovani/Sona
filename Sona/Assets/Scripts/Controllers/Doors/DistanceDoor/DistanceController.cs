using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceController : MonoBehaviour {

    public GameObject _door;
    OpenCloseManager _mechanism;

    void Start () {
        _mechanism = _door.GetComponent<OpenCloseManager>();
	}

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")){
            _mechanism.OpenCloseDoor(true);
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.CompareTag("Player")){
            _mechanism.OpenCloseDoor(false);
        }
    }

}
