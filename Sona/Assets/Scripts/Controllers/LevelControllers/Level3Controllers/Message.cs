using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class Message : MonoBehaviour {

    private Text onScreen;

    public void Awake(){
        onScreen = GetComponent<Text>();
        if (onScreen == null) {
            print ("MESSAGE: Falied finding textbox");
            return;
        }
        onScreen.text = "";
    }

    public void Show (String message){
        onScreen.text = message;
    }

    public void Erase (){
        onScreen.text = "";
    }
}
