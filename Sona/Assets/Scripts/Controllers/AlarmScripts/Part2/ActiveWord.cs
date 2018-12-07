using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveWord : MonoBehaviour {

    AlarmScheme _alarm;


    void Start()
    {
        _alarm = GetComponentInParent<AlarmScheme>();
    }

    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _alarm.DeActivePart2();
        }
    }
}
