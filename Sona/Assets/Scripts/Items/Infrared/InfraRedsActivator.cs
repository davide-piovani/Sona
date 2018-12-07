using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfraRedsActivator : MonoBehaviour {

    public GameObject _controller;

    InfraRedController _infraRedsController;

    void Start () {
        _infraRedsController = _controller.GetComponent<InfraRedController>();
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) {
            _infraRedsController.ActiveInfraReds();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _infraRedsController.ActiveInfraRedsMovement();
        }
    }
}
