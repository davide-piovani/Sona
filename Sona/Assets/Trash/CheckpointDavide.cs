using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ApplicationConstants;

public class CheckpointDavide : MonoBehaviour {

    private CheckpointController controller;
    [SerializeField] Quaternion rotation;
    [SerializeField] Material reachedColor;
    [SerializeField] Renderer checkpointLight;

    private void Start(){
        controller = FindObjectOfType<CheckpointController>();
    }

    private void OnTriggerEnter(Collider other){
        if (other.gameObject.tag == PlayersConstants.playerTag){
            print("Checkpoint raggiunto");
            Player player = other.gameObject.GetComponent<Player>();
            controller.NewCheckpointReached(player, transform.position, rotation);
            checkpointLight.material = reachedColor;
        }
    }

}
