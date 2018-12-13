using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour {

    struct PlayerCheckpoint {
        public String playerName;
        public bool reached;
        public Vector3 position;
        public Quaternion rotation;

        public void Initialize(Player player){
            this.playerName = player.name;
            this.reached = false;
            this.position = player.transform.position;
        }
    }

    private List<PlayerCheckpoint> playerCheckpoints = new List<PlayerCheckpoint>();
    protected int levelIndex;

    private void Awake() {
        levelIndex = FindObjectOfType<SceneLoader>().GetSceneIndex();
        var controllers = FindObjectsOfType<CheckpointController>();
        if (controllers.Length > 1){
            if (controllers[0].levelIndex == controllers[1].levelIndex){
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
        InitializeCheckpoints();
    }

    private void InitializeCheckpoints(){
        Player[] players = FindObjectsOfType<Player>();
        foreach(Player player in players){
            PlayerCheckpoint checkpoint = new PlayerCheckpoint();
            checkpoint.Initialize(player);
            playerCheckpoints.Add(checkpoint);
        }
    }

    public void NewCheckpointReached(Player player, Vector3 checkPos, Quaternion checkpointRotation){
        for(int i = 0; i < playerCheckpoints.Count; i++){
            PlayerCheckpoint checkpoint = playerCheckpoints[i];
            if (checkpoint.playerName == player.name) {
                checkpoint.reached = true;
                checkpoint.position = new Vector3(checkPos.x, player.transform.position.y, checkPos.z);
                checkpoint.rotation = checkpointRotation;
                playerCheckpoints[i] = checkpoint;
            }
        }
        printCheck();
    }

    public void printCheck()
    {
        foreach(PlayerCheckpoint c in playerCheckpoints)
        {
            print("Player: " + c.playerName + "   Reached: " + c.reached + "   Pos: " + c.position + "   Rotation:" + c.rotation);
        }
    }

    public void RestorePlayerCheckpoint(Player player){
        print("Restoring position");
        foreach (PlayerCheckpoint checkpoint in playerCheckpoints) {
            if (checkpoint.playerName == player.name && checkpoint.reached){
                print("Trovato player checkpoint for: " + player.name);
                player.transform.position = checkpoint.position;
                player.transform.rotation = checkpoint.rotation;
            }
        }
    }
}
