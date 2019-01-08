using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmLight : MonoBehaviour {

    private Light[] alarmLights;

    private void Start(){
        alarmLights = GetComponentsInChildren<Light>();
    }

    public void ToggleLights(){
        foreach(Light alarmLight in alarmLights){
            alarmLight.enabled = !alarmLight.enabled;
        }
    }

    public void ChangeState(bool active){
        foreach (Light alarmLight in alarmLights){
            alarmLight.enabled = active;
        }
    }
}
