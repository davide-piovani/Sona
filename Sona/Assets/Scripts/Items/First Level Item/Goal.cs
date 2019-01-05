using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ApplicationConstants;

public class Goal : MonoBehaviour {

    private SceneLoader sceneLoader;

    private void Start() {
        sceneLoader = FindObjectOfType<SceneLoader>();
    }

    private void OnCollisionEnter(Collision collision){
        if (collision.gameObject.tag == PlayersConstants.playerTag){
            CheckpointController controller = FindObjectOfType<CheckpointController>();
            if (controller) controller.InitializeCheckpoints();
            sceneLoader.LoadStartScene();
        }
    }
}
