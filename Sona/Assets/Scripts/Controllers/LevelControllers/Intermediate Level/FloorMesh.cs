using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FloorMesh : MonoBehaviour {


    Collider[] _colliders;
    bool playerOnFloor;
    public GameObject _player;

    void Start() {
        _colliders = GetComponentsInChildren<Collider>();
    }

    
    
}
