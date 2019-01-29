using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTutorial : MonoBehaviour {

    bool jack = false;
    bool hannah = false;
    bool charlie = false;
    TutorialManager _tutorial;

    void Start()
    {
        _tutorial = FindObjectOfType<TutorialManager>();
    }

    void Update () {
        if (jack & hannah & charlie) {
            _tutorial.LevelEnd();
        }
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) {
            if (other.gameObject.name.Equals("Jack")) { jack = true; }
            else if (other.gameObject.name.Equals("Hannah")) { hannah = true; }
            else if (other.gameObject.name.Equals("Charlie")) { charlie = true; }
            else { }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.gameObject.name.Equals("Jack")) { jack = false; }
            else if (other.gameObject.name.Equals("Hannah")) { hannah = false; }
            else if (other.gameObject.name.Equals("Charlie")) { charlie = false; }
            else { }
        }
    }
}
