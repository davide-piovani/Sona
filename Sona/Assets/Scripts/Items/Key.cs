using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : Interactable
{
    bool keyTaken;
    //InteractiveTextScript intarctionText;
    
    public override void Interact()
    {
        if (Input.GetButtonDown("InteractButton"))
        {
            keyTaken = true;
            Debug.Log("Key Taken");
        }    
    }

    public bool getKeyTaken()
    {
        return keyTaken;
    }
}
