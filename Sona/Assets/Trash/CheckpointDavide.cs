using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ApplicationConstants;

public class CheckpointDavide : MonoBehaviour {

    private GameController gameController;
    [SerializeField] Material checkpointLightColorWhenReached;
    [SerializeField] Renderer checkpointLight;

    private void Start(){
        gameController = FindObjectOfType<GameController>();
    }

    private void OnTriggerEnter(Collider other){
        if (other.gameObject.tag == PlayersConstants.playerTag){
            print("Checkpoint raggiunto");
            gameController.CheckpointReached();
            checkpointLight.material = checkpointLightColorWhenReached;
        }
    }

}
