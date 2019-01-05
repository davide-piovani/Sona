using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public Message outMessage;

    public void ShowMessage (String message, int priority){
        outMessage.Show (message);
    }

    public void EraseMessage (){
        outMessage.Erase();
    }
}
