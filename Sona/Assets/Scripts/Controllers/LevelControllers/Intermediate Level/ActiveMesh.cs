using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ActiveMesh : MonoBehaviour {

    public GameObject _player;
    NavMeshAgent _navMesh;

    void Start() {
        _navMesh = _player.GetComponent<NavMeshAgent>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) {
            other.GetComponent<NavMeshAgent>().enabled = false;
        }
    }

    void OnTriggerExit(Collider other) {

        if (other.CompareTag("Player"))
        {
            other.GetComponent<NavMeshAgent>().enabled = true;
        }
    }

}
