using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveCharacterController : MonoBehaviour {
    [SerializeField] Player[] players;
    private int current = 0;
    public int startingPlayer;

    //this array states if the player with the same index is allowed to be selected
    private bool[] on;

    // Use this for initialization
    void Awake () {
        on = new bool[players.Length];
        for (int i = 0; i < players.Length; i++){
            on[i] = true;
        }
    }

    public void DeactiveAll(){
        for (int i = 0; i < players.Length; i++){
            players[i].Deactivate();
        }
    }

    private void Start(){
        /*for (int i = 0; i < players.Length; i++){
            players[i].Deactivate();
        }
        ActivePlayer(startingPlayer);*/
    }

    private void ActivePlayer(int index){
        if (current != -1) players[current].Deactivate();
        current = index;
        players[index].Activate();
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

    //Returns the currently active player if it exists, else it returns null
    public Player GetCurrentlyActive (){
        if (current != -1){
            if (on[current]){
                return(players[current]);
            }
        }
        return null;
    }

    //Allows to set the starting player. Returns it
    public Player SetStartingPlayer(PlayerType type) {
        for(int i = 0; i < players.Length; i++) {
            if (players[i].GetPlayerType() == type){
                startingPlayer = i;
                current = i;
                return players[i];
            }
        }
        return null;
    }

    //Activate the next on player. Returns player index on success, -1 if no player is on
    private int NextPlayerIndex(){
        int currentIndex = current;
        for(int i = 0; i < players.Length; i++){
            currentIndex = (currentIndex + 1) % players.Length;
            print (currentIndex + ", " + on[currentIndex]);
            if (on[currentIndex]) return currentIndex;
        }
        current = -1;
        //print ("-1");
        return (-1);
    }

    public Player GiveControlToNextPlayer(){
        int playerIndex = NextPlayerIndex();
        if (playerIndex < 0) {
            //print ("no player found");
            return null;
        }

        ActivePlayer(playerIndex);
        return players[current];
    }

    //Disables the player whose index had been given, if the player is the currently active one, it switch to the next on player
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
        if (current == -1){
            current = 0;
        }
        on[current] = true;
        players[current].Activate();
        return(current);
    }
}
