using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentBox : Interactable {

    public Level3Manager manager;
    private bool active = false;

    public override void Interact (){
        print ("BOX: interacting");
        manager.EraseMessage();
        manager.RetrieveComponent(player);
    }

    protected override void ShowTooltip (){
        active = true;
        manager.ShowMessage("Collect", 0);
    }

    protected override void HideTooltip() {
        if(active){
            manager.EraseMessage();
            active = false;
        }
    }
}
