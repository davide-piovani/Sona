using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour {

    public TextMeshProUGUI dial_content;
    public Text dial_name;

    public void ShowDial(String name, String content){
        dial_content.text = content;
        dial_name.text = name;
    }

    public void Erase() {
        dial_content.text = "";
        dial_name.text = "";
    }
}
