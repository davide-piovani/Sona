using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour {

    public Checkpoint[] checkpoints;
    private int lastCheckpoint = 0;

    private void Awake() {
        var controllers = FindObjectsOfType<CheckpointController>();
        if (controllers.Length > 1){
            if (EqualControllers(controllers[0], controllers[1])){
                Destroy(gameObject);
            } else {
                Destroy(gameObject == controllers[0] ? controllers[1] : controllers[0]);
                DontDestroyOnLoad(gameObject);
            }
        } else {
            DontDestroyOnLoad(gameObject);
        }
    }

    private bool EqualControllers(CheckpointController c1, CheckpointController c2){
        if (c1.checkpoints.Length != c2.checkpoints.Length) return false;
        for(int i = 0; i < c1.checkpoints.Length; i++){
            if (c1.checkpoints[i] != c2.checkpoints[i]) return false;
        }
        return true;
    }

    private int GetCheckpointIndex(Checkpoint checkpoint){
        for (int i = 0; i < checkpoints.Length; i++){
            if (checkpoints[i] == checkpoint) return i;
        }
        return 0;
    }

    public void AddCheckpoint(Checkpoint checkpoint){
        int checkpointIndex = GetCheckpointIndex(checkpoint);
        lastCheckpoint = (checkpointIndex > lastCheckpoint) ? checkpointIndex : lastCheckpoint;
    }

    public void LoadLastCheckPoint(){
        FindObjectOfType<SceneLoader>().ReloadCurrentScene();
    }
}
