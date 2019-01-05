using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : Interactable {

    public int id;
    public AutomatedDoor door;
    public bool power = false;
    private bool active = false;
    public Level3Manager manager;

    public GameObject interactingPlayer = null;


    public override void Interact (){
        print ("SWITCH " + id + " interacting");
        if (power){
            door.Open(id);
            interactingPlayer = player;
        } else {
            resetInteraction();
        }
    }

    protected override void ShowTooltip () {
        active = true;
        if (power) {
            manager.ShowMessage("Activate", 0);
        }
    }

    protected override void HideTooltip () {
        if (active){
            manager.EraseMessage();
            active = false;
        }
    }

    public void OnCollisionExit (Collision collision){
        if (collision.collider.gameObject == interactingPlayer){
            door.Close(id);
            interactingPlayer = null;
            resetInteraction();
        }
    }
}
