using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour {

    public List<CheckpointDavide> checkpointsReached = new List<CheckpointDavide>();

    private void Awake() {
        var controllers = FindObjectsOfType<CheckpointController>();
        if (controllers.Length > 1){
            if (EqualControllers(controllers[0], controllers[1])){
                Destroy(this.gameObject);
            } else {
                Destroy(gameObject == controllers[0] ? controllers[1] : controllers[0]);
                DontDestroyOnLoad(this.gameObject);
            }
        } else {
            DontDestroyOnLoad(this.gameObject);
        }
    }

    private void Start(){
        CheckpointDavide lastCheckpoint = GetLastCheckpoint();
        if (lastCheckpoint != null){
            FindObjectOfType<Player>().transform.position = lastCheckpoint.transform.position;
        }
    }

    private bool EqualControllers(CheckpointController c1, CheckpointController c2){
        if (c1.checkpointsReached.Count != c2.checkpointsReached.Count) return false;
        for(int i = 0; i < c1.checkpointsReached.Count; i++){
            if (c1.checkpointsReached[i] != c2.checkpointsReached[i]) return false;
        }
        return true;
    }

    public void AddCheckpoint(CheckpointDavide checkpoint){
        checkpointsReached.Add(checkpoint);
    }

    private CheckpointDavide GetLastCheckpoint(){
        if (checkpointsReached.Count == 0) return null;
        CheckpointDavide lastCheckpoint = checkpointsReached[0];
        foreach(CheckpointDavide checkpoint in checkpointsReached){
            if (checkpoint.GetIndex() > lastCheckpoint.GetIndex()) lastCheckpoint = checkpoint;
        }
        return lastCheckpoint;
    }

    public void LoadLastCheckPoint(){
        FindObjectOfType<SceneLoader>().ReloadCurrentScene();
    }
}
