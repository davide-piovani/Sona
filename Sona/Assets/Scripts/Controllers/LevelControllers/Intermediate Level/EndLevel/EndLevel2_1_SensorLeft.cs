using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevel2_1_SensorLeft : MonoBehaviour {

    EndLevel2_1 _endLevel;
    bool hannah = false;

    void Start() {
        _endLevel = FindObjectOfType<EndLevel2_1>();
    }

    void Update() {
        if (hannah)
        {
            _endLevel.SensorL_Active(true);
        }
        else
        {
            _endLevel.SensorL_Active(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.gameObject.name.Equals("Hannah")) { hannah = true; }
            else { }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.gameObject.name.Equals("Hannah")) { hannah = false; }
            else { }
        }
    }
}
