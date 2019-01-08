using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastDoorOpener : Interactable {

    public override void Interact(){
        gameController.lastDoorOpen = true;
    }
}
