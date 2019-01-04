using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour {

    private Light light;

    // Use this for initialization
    void Awake () {
        light = GetComponentInChildren<Light>();
        if (light == null){
            print ("no light found");
        }
    }
	
    public void ToEmergency (){
        if (light == null){
            //print (">>>>>>No light here");
            return;
        }
        light.intensity = 0.4f;
        light.color = Color.red;
    }

    public void TurnOff (){
        if (light == null){
            print (">>>>>>No light here");
            return;
        }
        light.intensity = 0f;
    }

    public void Restore (){
        if (light == null){
            //print (">>>>>>No light here");
            return;
        }
        light.intensity = 1f;
        light.color = Color.white;
    }
}
