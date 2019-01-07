using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour {

    private Light componentLight;

    // Use this for initialization
    void Awake () {
        componentLight = GetComponentInChildren<Light>();
        if (componentLight == null){
            print ("no light found");
        }
    }
	
    public void ToEmergency (){
        if (componentLight == null){
            //print (">>>>>>No light here");
            return;
        }
        componentLight.intensity = 0.4f;
        componentLight.color = Color.red;
    }

    public void TurnOff (){
        if (componentLight == null){
            print (">>>>>>No light here");
            return;
        }
        componentLight.intensity = 0f;
    }

    public void Restore (){
        if (componentLight == null){
            //print (">>>>>>No light here");
            return;
        }
        componentLight.intensity = 1f;
        componentLight.color = Color.white;
    }
}
