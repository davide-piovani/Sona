using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevel2_1_SensorRight : MonoBehaviour {

    public ButtonDoor _door;
    EndLevel2_1 _endLevel;

    bool jack = false;
    bool charlie = false;

    void Start() {
        _endLevel = FindObjectOfType<EndLevel2_1>();
    }

    void Update() {
        if (jack & charlie & _door.isUnlocked())
        {
            _endLevel.SensorR_Active(true);
        }
        else {
            _endLevel.SensorR_Active(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.gameObject.name.Equals("Jack")) { jack = true; }
            else if (other.gameObject.name.Equals("Charlie")) { charlie = true; }
            else { }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.gameObject.name.Equals("Jack")) { jack = false; }
            else if (other.gameObject.name.Equals("Charlie")) { charlie = false; }
            else { }
        }
    }

}
