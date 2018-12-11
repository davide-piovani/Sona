using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ApplicationConstants;

public class Checkpoint : MonoBehaviour {

    public bool reached = false;
    private CheckpointController controller;

    private void Start(){
        controller = FindObjectOfType<CheckpointController>();
    }

    private void OnTriggerEnter(Collider other){
        if (other.gameObject.tag == PlayersConstants.playerTag){
            reached = true;
            controller.AddCheckpoint(this);
        }
    }
}
