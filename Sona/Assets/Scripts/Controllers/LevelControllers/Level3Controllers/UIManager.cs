using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public Level3Manager manager;
    public Message outMessage;
    public DialogueManager dial;
    public DocManager doc;

    public void ShowMessage (String message, int priority){
        outMessage.Show (message);
    }

    public void EraseMessage (){
        outMessage.Erase();
    }

    public void ShowDocument(String title, String content) {
        doc.gameObject.SetActive(true);
        doc.ShowDoc(title, content);
    }

    public void NextDocument() {
        doc.gameObject.SetActive(false);
        manager.Next();
    }

    public void ShowDial(String name, String content) {
        dial.gameObject.SetActive(true);
        dial.ShowDial(name, content);
    }

    public void NextDial () {
        dial.gameObject.SetActive (false);
        manager.Next();
    }
}
