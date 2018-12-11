using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ApplicationConstants;

public class CheckpointDavide : MonoBehaviour {

    [SerializeField] int index;
    private CheckpointController controller;

    private void Start(){
        controller = FindObjectOfType<CheckpointController>();
    }

    private void OnTriggerEnter(Collider other){
        if (other.gameObject.tag == PlayersConstants.playerTag){
            print("Checkpoint raggiunto");
            controller.AddCheckpoint(this);
        }
    }

    public int GetIndex() { return index; }
}
