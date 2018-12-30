using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayActivator : MonoBehaviour {

    InteractiveTextScript _text;
    GameObject _player;
    bool active = false;
    bool necessary = true;

    void Start()
    {
        _text = GetComponentInChildren<InteractiveTextScript>();
    }

    void Update()
    {
        if (active & necessary)
        {
            _text.ShowText();
        }
        else {
            _text.HideText();
        }
    }

    public bool IsActive() {
        return active;
    }

    public void Necessary(bool cond) {
        necessary = cond;
    }

    public GameObject GetPlayer() {
        return _player;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") & other.gameObject.GetComponentInChildren<Camera>().enabled == true /*player is active*/) {
            active = true;
            _player = other.gameObject;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player")) {

            if (other.gameObject.GetComponentInChildren<Camera>().enabled == false /*player is not active*/) {
                active = false;
            }
            else {
                active = true;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            active = false;
            _player = null;
        }
    }

}
