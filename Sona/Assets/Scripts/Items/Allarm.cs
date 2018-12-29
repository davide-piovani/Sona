using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Allarm : Interactable{

    //If player interact with the allarm it rings and call for guards
    public override void Interact()
    {
        Debug.Log("You pressed the allarm!");
        EventManager.AllarmRinging();
    }

}
