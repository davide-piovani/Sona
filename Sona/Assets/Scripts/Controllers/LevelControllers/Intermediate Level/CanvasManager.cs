using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour {

    public TutorialManager manager;
    public DialogueManager dial;
    public Text msg;
    public Image powerBarCircle;
    public Image powerBarIcon;

    public void DialogueWindowActive(bool cond) {
        dial.gameObject.SetActive(cond);
    }

    public void ShowDial(string name, string content)
    {
        dial.gameObject.SetActive(true);
        dial.ShowDial(name, content);
    }

    public void NextDial()
    {
        dial.gameObject.SetActive(false);
        manager.Next();
    }

    public void PowerBarActive(bool cond)
    {
        powerBarCircle.enabled = cond;
        powerBarIcon.enabled = cond;
    }

    public void MSGEnabled(bool cond) {
        msg.enabled = cond;
    }

    public void ShowMessage(string txt) {
        msg.text = txt;
    }

    public void EraseMessage() {
        msg.text = "";
    }
}
