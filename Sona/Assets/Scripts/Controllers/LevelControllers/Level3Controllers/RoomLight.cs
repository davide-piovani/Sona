using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomLight : MonoBehaviour {

    public LightController[] lights;
    public bool emergency;

    public void ShutDown (){
        int i;
        if (emergency){
            for (i=0; i<lights.Length; i++){
                lights[i].ToEmergency();
            }
        } else {
            for (i=0; i<lights.Length; i++){
                lights[i].TurnOff();
            }
        }
    }

    public void Restore (){
        for(int i=0; i<lights.Length; i++){
            lights[i].Restore();
        }
    }
}
