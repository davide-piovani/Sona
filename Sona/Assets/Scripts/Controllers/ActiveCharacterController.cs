using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveCharacterController : MonoBehaviour {
    public Player [] players = new Player [3];
    private int current;
    private bool [] on = new bool [3];

    void Awake (){
        current = 2;
        for(int i = 0; i<3; i++){
            players[i].Deactivate();
            on[i] = true;
        }
    }

    // Use this for initialization
    void Start () {
	
    }

    //Activate the next on player. Returns player index on success, -1 if no player is on
    public int NextPlayer(){
        int i;
        players[current].Deactivate();
        for(i=0; i<3; i++){
            current = (current + 1) %3;
            if (on[current]){
                players[current].Activate();
                return (current);
            }
        }
        return (-1);
    }

    //Disables the player whose index had been given, if the player is the currently active one, it switch to the next on player
    public int Deactivate (int index){
        on [index] = false;
        if (index == current){
            return(NextPlayer());
        }
        return (current);
    }

    //Disables all players. Doesn't change the index of the last active player
    public void DeactivateAll (){
        int i;
        players[current].Deactivate();
        for (i=0; i<3; i++){
            on [i] = false;
        }
    }

    //Disables the currently active player. Performs the player switch
    public int DeactivateCurrent (){
        return (Deactivate(current));
    }

    //Enables the player whose index has been given
    public void Activate (int index){
        on [index] = true;
    }

    //Enables all players, doesn't activate anyone
    public void ActivateAll (){
        int i;
        for (i=0; i<3; i++){
            on [i] = true;
        }
    }

    //Enables and activates the last active player.
    public int ActivateCurrent (){
        on[current] = true;
        players[current].Activate();
        return(current);
    }
	
	// Update is called once per frame
	/*void Update () {
		
	}*/
}
