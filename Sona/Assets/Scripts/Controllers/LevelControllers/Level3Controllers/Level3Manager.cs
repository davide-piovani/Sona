using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using ApplicationConstants;
using UnityStandardAssets.CrossPlatformInput;

public class Level3Manager : MonoBehaviour {

    private RoomLight[] lights;
    private Interactable[] int_obj;
    //public ActiveCharacterController character;
    public Player currentPlayer;
    private Vector3[] positions = new Vector3[3] {new Vector3 (-40.5f, -0.65f, 0.2f), new Vector3 (-37f, -0.65f, -3f), new Vector3(14.49f, 0.2f, 28.539f)};
    public ComponentBox box;
    public GeneralSwitch gen;
    public Circuitry circ;
    public UIManager toScreen;
    private GameController controller;

    // Use this for initialization
    void Start () {
        int i;
        lights = FindObjectsOfType<RoomLight>();
        controller = FindObjectOfType<GameController>();
        Door[] doors = FindObjectsOfType<Door>();
        Switch[] switches = FindObjectsOfType <Switch>();
        int_obj = new Interactable[doors.Length + switches.Length];
        System.Array.Copy(doors, int_obj, doors.Length);
        System.Array.Copy(switches, 0, int_obj, doors.Length, switches.Length);
        for (i=0; i<lights.Length; i++){
            lights[i].ShutDown();
        }
        for (i=0; i< doors.Length; i++){
            doors[i].SetManager(this);
        }

        for (i=0; i<switches.Length; i++){
            switches[i].manager = this;
        }
        box.GetComponent<Transform>().position = positions[UnityEngine.Random.Range(0,2)];
    }

    void Update () {
        int i;
        if (!(controller.GetActivePlayer() == currentPlayer)){
            currentPlayer = controller.GetActivePlayer();
            for (i=0; i<int_obj.Length; i++){
                int_obj[i].player = currentPlayer.gameObject;
            }
            box.player = currentPlayer.gameObject;
            gen.player = currentPlayer.gameObject;
            circ.player = currentPlayer.gameObject;
            if (currentPlayer == null){
                print ("LEVEL MANAGER: Couldn't get player");
            }
        }
        
    }

    public void ActivateTorch() {
        Player[] players = controller.GetScenePlayers();
        for (int i=0; i<players.Length; i++){
            if (players[i].GetPlayerType() == PlayerType.Hannah){
                players[i].GetComponent<Animator>().SetBool("hasTorch", true);
                return;
            }
        }
    }

    public void RetrieveComponent(GameObject retriever){
        print("LEVEL MANAGER: called for retrieve component");
        Destroy (box.gameObject);
        circ.SetRepairer(currentPlayer.gameObject);
    }

    public void ShutDown (){
        print("LEVEL MANAGER: called for shut down");
        for(int i = 0; i < lights.Length; i++){
            lights[i].emergency = false;
            lights[i].ShutDown();
        }
        circ.shutDown = true;
    }

    public void RestorePower (){
        print("LEVEL MANAGER: called for restore power");
        Switch s;
        for(int i = 0; i < lights.Length; i++){
            lights[i].Restore();
        }

        for(int i = 0; i < int_obj.Length; i++){
            s = int_obj[i] as Switch;
            if (s != null){
                s.power = true;
            }
        }
    }

    public void repair (){
        gen.repaired = true;
    }

    //Show message on the center of the screen
    public void ShowMessage (String message, int priority){
        toScreen.ShowMessage (message, priority);
    }

    //Erase message from screen
    public void EraseMessage (){
        toScreen.EraseMessage();
    }

}
