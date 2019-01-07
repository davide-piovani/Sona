using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Experimental.GlobalIllumination;

public class Circuitry : Interactable {
    public Level3Manager manager;
    private GameObject hasComponent;
    private bool component = false;
    public bool shutDown = false;
    private bool active = false;
    private bool repaired = false;
    

    public override void Interact(){
        if (!component && shutDown){
            print ("placing component");
            if (player == hasComponent){
                hasComponent = null;
                component = true;
            }
            resetInteraction();
        } else if(shutDown) {
            print ("repairing");
            repaired = true;
            manager.repair();
        } else {
            resetInteraction();
        }
    }

    public void SetRepairer (GameObject player){
        hasComponent = player;
    }

    protected override void ShowTooltip (){
        active = true;
        if (!component && hasComponent == null){
            manager.ShowMessage ("Component needed", 0);

        } else if (!component && !(hasComponent == null)){
            if(shutDown){
                manager.SendMessage("Turn off the power first");
            } else {
                if (player == hasComponent){
                    manager.ShowMessage("Place component", 0);
                } else {
                    manager.ShowMessage("Not with component", 1);
                }
            }
        } else if (component && !repaired){
                manager.ShowMessage("Repair", 0);
        }
    }

    protected override void HideTooltip() {
        if(active){
            manager.EraseMessage();
            active = false;
        }
    }
	
}
