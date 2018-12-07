using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopWord : MonoBehaviour {

    AlarmScheme _alarm;


    void Start () {
        _alarm = GetComponentInParent<AlarmScheme>();
    }
	
	void Update () {
		
	}

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            _alarm.ActivePart2();
        }
    }


}
