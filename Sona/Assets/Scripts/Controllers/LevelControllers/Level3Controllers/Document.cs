using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Document : Interactable {

    public Level3Manager manager;
    private bool active = false;

    public override void Interact () {
         manager.ShowDoc();
    }

    protected override void ShowTooltip (){
        active = true;
        manager.ShowMessage("Read", 0);
    }

    protected override void HideTooltip() {
        if(active){
            manager.EraseMessage();
            active = false;
        }
    }
}
