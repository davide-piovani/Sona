using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayActivator : MonoBehaviour {

    GameObject _player;
    bool active = false;

    public bool IsActive() {
        return active;
    }

    public GameObject GetPlayer() {
        return _player;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            active = true;
            _player = other.gameObject;
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
