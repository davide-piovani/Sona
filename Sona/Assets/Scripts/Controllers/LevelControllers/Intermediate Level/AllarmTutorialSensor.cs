using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllarmTutorialSensor : MonoBehaviour {

    TutorialManager _tutorial;

    void Start()
    {
        _tutorial = FindObjectOfType<TutorialManager>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") & other.gameObject.name.Equals("Hannah"))
        {
            _tutorial.CloseToAllarm();
            gameObject.SetActive(false);
        }

    }
}
