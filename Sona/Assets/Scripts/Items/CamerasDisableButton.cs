using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerasDisableButton : Interactable {

    Allarm allarm;

	// Use this for initialization
	new void Start () {
        base.Start();
        allarm = FindObjectOfType<Allarm>();
    }

    public override void Interact()
    {
        allarm.deactiveAllarm();
    }
}
