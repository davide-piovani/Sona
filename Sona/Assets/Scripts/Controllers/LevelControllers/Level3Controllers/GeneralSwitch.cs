using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralSwitch : Interactable {

    public Level3Manager manager;
    private bool power = true;
    public bool repaired = false;
    private bool active = false;

    public override void Interact () {
        if (power){
            manager.ShutDown();
            resetInteraction();
            power = false;
        } else if (repaired) {
            manager.RestorePower();
        } else {
            resetInteraction();
        }
    }

    void Awake () {
        radius = 2;
    }

    protected override void ShowTooltip (){
        active = true;
        if (power){
            manager.ShowMessage("Press b to turn off the power", 0);
        }
    }

    protected override void HideTooltip() {
        if(active){
            manager.EraseMessage();
            active = false;
        }
    }
}
