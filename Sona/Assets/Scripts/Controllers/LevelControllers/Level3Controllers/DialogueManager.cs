using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour {

    public Text dial_content;
    public Text dial_name;
    public Image icon;
    [SerializeField] Sprite jack;
    [SerializeField] Sprite hannah;
    [SerializeField] Sprite charlie;

    int currentPosition = 0;
    float delay = 0.001f;  // 10 characters per sec.
    string tmpText = "";
    
    public void ShowDial(String name, String content){
        WriteText(content);
        dial_name.text = name+": ";
        positionTextAndSetIcon(name);
        StartCoroutine(ShowText());
        //dial_content.text = content;
    }

    void positionTextAndSetIcon(string name) {
        float y, z;
        y = dial_content.rectTransform.localPosition.y;
        z = dial_content.rectTransform.localPosition.z;
        if (name.Equals("Jack")) {
            icon.sprite = jack;
            dial_content.rectTransform.localPosition = new Vector3(-560, y, z);
        }
        else if (name.Equals("Hannah"))
        {
            icon.sprite = hannah;
            dial_content.rectTransform.localPosition = new Vector3(-500, y, z);
        }
        else if (name.Equals("Charlie"))
        {
            icon.sprite = charlie;
            dial_content.rectTransform.localPosition = new Vector3(-515, y, z);
        }
    }

    public void Erase() {
        dial_content.text = "";
        dial_name.text = "";
    }

    void WriteText(string aText) {
        dial_content.text = "";
        currentPosition = 0;
        tmpText = aText;
    }

    IEnumerator ShowText() {
        while (true)
        {
            if (currentPosition < tmpText.Length) dial_content.text += tmpText[currentPosition++];
            yield return new WaitForSeconds(delay);
        }
    }
}
