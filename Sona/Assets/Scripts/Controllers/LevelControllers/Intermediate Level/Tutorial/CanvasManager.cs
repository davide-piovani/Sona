using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class CanvasManager : MonoBehaviour {

    public TutorialManager manager;
    public DialogueManager dial;
    public StreamVideo tutorialMSG;
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
        tutorialMSG.gameObject.SetActive(cond);
    }

    public void ShowTutorialVideoMessage(string tutorialTxt, string pressTxt, string button, VideoClip video) {
        tutorialMSG.setVideoSource(video);
        tutorialMSG.Play();
        tutorialMSG.ShowMessage(tutorialTxt, button);
        tutorialMSG.SetPressText(pressTxt, button);
    }

    public void EraseTutorialVideoMessage() {
        tutorialMSG.setVideoSource(null);
        tutorialMSG.EraseMessage();
        tutorialMSG.ErasePressText();
    }

    public void ShowTutorialMessage(string txt)
    {
        msg.text = txt;
    }

    public void EraseTutorialMessage()
    {
        msg.text = "";
    }

    public void ShowPressText(string text, string button) {
        tutorialMSG.SetPressText(text, button);
    }

    public void ErasePressText() {
        tutorialMSG.ErasePressText();
    }
}
