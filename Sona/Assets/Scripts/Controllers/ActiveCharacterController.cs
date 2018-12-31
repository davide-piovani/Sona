using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveCharacterController : MonoBehaviour {
    [SerializeField] Player[] players;
    private int current = 0;
    private bool[] on;

    // Use this for initialization
    void Awake () {
        on = new bool[players.Length];

        for (int i = 0; i < players.Length; i++){
            players[i].Deactivate();
            on[i] = true;
        }

        ActivePlayer(0);
    }

    private void ActivePlayer(int index){
        players[current].Deactivate();
        current = index;
        players[index].Activate();
        //on[index] = true;
    }

    public Player[] GetScenePlayers() { return players; }

    //Active the player of the type passed if it could be activated,
    //otherwise return the current player
    public Player ActivePlayerOfType(PlayerType type){
        for(int i = 0; i < players.Length; i++) {
            if (players[i].GetPlayerType() == type && on[i]){
                ActivePlayer(i);
                return players[i];
            }
        }
        return players[current];
    }

    //Activate the next on player. Returns player index on success, -1 if no player is on
    private int NextPlayerIndex(){
        int currentIndex = current;
        for(int i = 0; i < players.Length; i++){
            currentIndex = (currentIndex + 1) % players.Length;
            print (currentIndex + ", " + on[currentIndex]);
            if (on[currentIndex]) return currentIndex;
        }
        print ("-1");
        return (-1);
    }

    public Player GiveControlToNextPlayer(){
        int playerIndex = NextPlayerIndex();
        if (playerIndex < 0) return null;

        ActivePlayer(playerIndex);
        return players[current];
    }

    //Disables the player whose index had been given, if the player is the currently active one, it switch to the next on player
    /*public int Disable (int index){
        on [index] = false;
        if (index == current){
            return(NextPlayerIndex());
        }
        return (current);
    }*/
    public Player Disable (int index){
        on [index] = false;
        if (index == current){
            return(GiveControlToNextPlayer());
        }
        return (players[current]);
    }

    //Disables all players. Doesn't change the index of the last active player
    public void DisableAll (){
        int i;
        players[current].Deactivate();
        for (i=0; i<players.Length; i++){
            on [i] = false;
        }
    }

    //Disables the currently active player. Performs the player switch
    /*public int DisableCurrent (){
        return (Disable(current));
    }*/
    public Player DisableCurrent (){
        return (Disable(current));
    }

    //Enables the player whose index has been given
    public void Enable (int index){
        on [index] = true;
    }

    //Enables all players, doesn't activate anyone
    public void EnableAll (){
        int i;
        for (i=0; i<players.Length; i++){
            on [i] = true;
        }
    }

    //Enables and activates the last active player.
    public int EnableCurrent (){
        on[current] = true;
        players[current].Activate();
        return(current);
    }
	
}
