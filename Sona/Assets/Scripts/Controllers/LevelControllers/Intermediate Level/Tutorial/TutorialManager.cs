using ApplicationConstants;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityStandardAssets.CrossPlatformInput;

public class TutorialManager : MonoBehaviour {

    Player currentPlayer;
    public InteractController _doorController;
    public CanvasManager toScreen;
    public TutorialTexts lines;
    public Camera freeCam;
    public VideoClip video1;
    public VideoClip video2;
    public VideoClip video3;

    private GameController controller;

    int state = 0;
    bool newMessage;
    bool doorPassed = false;
    bool doorOpen = false;
    bool closeToAllarm = false;
    bool allarmDeactivated = false;
    bool levelEnd = false;
    bool waiting = false;
    bool active = false;

    SceneLoader _sceneLoader;
    FadeInOut _fadeInOut;

    void Start()
    {
        freeCam.enabled = false;
        InitObjects();
        StartCoroutine(StartDialogue());
    }

    void InitObjects()
    {
        _fadeInOut = FindObjectOfType<FadeInOut>();
        _sceneLoader = FindObjectOfType<SceneLoader>();
        controller = FindObjectOfType<GameController>();
        toScreen.DialogueWindowActive(false);
        toScreen.MSGEnabled(false);
        controller.ChangePlayerActive(false);
        foreach (Player p in controller.GetScenePlayers()) { p.CanUsePower(false); }
    }

    void Update()
    {
        if (active)
        {
            if (!waiting)
            {
                switch (state)
                {
                    case 0:
                        freeCam.gameObject.transform.position = new Vector3(-6f, 2f, 0f);
                        freeCam.gameObject.transform.rotation = Quaternion.Euler(0, 90, 0);
                        controller.DisableInput();
                        SetPlayer();
                        toScreen.PowerBarActive(false);
                        currentPlayer.Deactivate();
                        freeCam.enabled = true;
                        ShowDialogueLine(3);
                        if (state == 1)
                        {
                            freeCam.enabled = false;
                            toScreen.PowerBarActive(true);
                            currentPlayer.Activate();
                            controller.ActiveInput();
                        }
                        break;
                    case 1:
                        controller.ChangePlayerActive(true);
                        toScreen.MSGEnabled(true);
                        toScreen.ShowTutorialVideoMessage(
                            "You can control only one character at a time.\nSwitch among the characters until charlie is selected",
                            "Press          to change character",
                            "CHANGE_CHARACTER", video1);
                        state++;
                        break;
                    case 2:
                        if (GameSettings.GetButtonDown(PlayersConstants.changeCharacterButton))
                        {
                            toScreen.EraseTutorialVideoMessage();
                            toScreen.ShowPressText("Press          to change character", "CHANGE_CHARACTER");
                            state++;
                        }
                        break;
                    case 3:
                        if (GameSettings.GetButtonDown(PlayersConstants.changeCharacterButton))
                        {
                            toScreen.ErasePressText();
                            toScreen.MSGEnabled(false);
                            state++;
                        }
                        break;
                    case 4:
                        if (controller.GetActivePlayer().name.Equals("Charlie"))
                        {
                            controller.ChangePlayerActive(false);
                            controller.GetActivePlayer().CanUsePower(true);
                            toScreen.MSGEnabled(true);
                            toScreen.ShowTutorialVideoMessage(
                                "Charlie has the power of pass through particular objects and materials.\nAs you may have noticed the door became blue!\nThis mean that Charlie can use his super power to pass through it.Try!",
                                "Press          to activate power",
                                "ACTIVE_POWER", video2);
                            state++;
                        }
                        break;
                    case 5:
                        if (GameSettings.GetButtonDown(PlayersConstants.powerButtonName))
                        {
                            controller.ChangePlayerActive(true);
                            toScreen.EraseTutorialVideoMessage();
                            toScreen.MSGEnabled(false);
                            state++;
                        }
                        break;
                    case 6:
                        if (doorPassed) {
                            freeCam.gameObject.transform.position = new Vector3(7.5f, 2f, 0f);
                            freeCam.gameObject.transform.rotation = Quaternion.Euler(20, 90, 0);
                            controller.DisableInput();
                            SetPlayer();
                            toScreen.PowerBarActive(false);
                            currentPlayer.Deactivate();
                            freeCam.enabled = true;
                            ShowDialogueLine(4);
                        }
                        if (state == 7)
                        {
                            freeCam.enabled = false;
                            toScreen.PowerBarActive(true);
                            currentPlayer.Activate();
                            controller.ActiveInput();
                        }
                        break;
                    case 7:
                        if (doorOpen)
                        {
                            _doorController.Necessary(false);
                            _doorController.ForceDeActive();
                            freeCam.gameObject.transform.position = new Vector3(-6f, 2f, 0f);
                            freeCam.gameObject.transform.rotation = Quaternion.Euler(0, 90, 0);
                            controller.DisableInput();
                            SetPlayer();
                            toScreen.PowerBarActive(false);
                            currentPlayer.Deactivate();
                            freeCam.enabled = true;
                            ShowDialogueLine(5);
                        }
                        if (state == 8)
                        {
                            _doorController.ReActive();
                            _doorController.Necessary(true);
                            freeCam.enabled = false;
                            toScreen.PowerBarActive(true);
                            currentPlayer.Activate();
                            controller.ActiveInput();
                            toScreen.ShowTutorialMessage("Use Hannah to explore the rest of the area!");
                        }
                        break;
                    case 8:
                        if (controller.GetActivePlayer().name.Equals("Hannah")) {
                            toScreen.EraseTutorialMessage();
                        }
                        if (closeToAllarm) {
                            controller.ChangePlayerActive(false);
                            controller.GetActivePlayer().CanUsePower(true);
                            toScreen.MSGEnabled(true);
                            toScreen.ShowTutorialVideoMessage(
                                "Ok, in front of you there is an area under video surveillance.\nHannah has the ability of became invisible.\nTry to pass the area without being seen and deactivate the videocameras\npressing the button at the end of the hallway, next to the door",
                                "Press          to activate power",
                                "ACTIVE_POWER", video3);
                            state++;
                        }
                        break;
                    case 9:
                        if (GameSettings.GetButtonDown(PlayersConstants.powerButtonName))
                        {
                            controller.ChangePlayerActive(true);
                            toScreen.EraseTutorialVideoMessage();
                            toScreen.MSGEnabled(false);
                            state++;
                        }
                        break;
                    case 10:
                        if (allarmDeactivated) {
                            toScreen.ShowTutorialMessage("Good job! Now bring the other characters\nnear Hannah to leave the room.");
                            state++;
                        }
                        break;
                    case 11:
                        if (GameSettings.GetButtonDown(PlayersConstants.changeCharacterButton))
                        {
                            toScreen.EraseTutorialMessage();
                            state++;
                        }
                        break;
                    case 12:
                        if (levelEnd) {
                            StartCoroutine(Restart());
                        }
                        break;
                }
            }
            else
            {
                if (GameSettings.GetButtonDown(PlayersConstants.interactButton))
                {
                    toScreen.NextDial();
                }
            }
        }
    }

    private void SetPlayer()
    {
        currentPlayer = controller.GetActivePlayer();
    }    
    
    public void ShowDoc()
    {
        state = 4;
    }

    private void ShowDialogueLine(int end)
    {
        string name = lines.GetName(end);
        string line = lines.GetDialContent(end);

        if (name != null && line != null)
        {
            toScreen.ShowDial(name, line);
            waiting = true;
        }
        else
        {
            state++;
        }
    }

    public void DoorPassed() {
        doorPassed = true;
    }

    public void DoorOpen()
    {
        doorOpen = true;
    }

    public void CloseToAllarm()
    {
        closeToAllarm = true;
    }

    public void AllarmDeactivated()
    {
        allarmDeactivated = true;
    }

    public void LevelEnd()
    {
        levelEnd = true;
    }

    public void Next()
    {
        waiting = false;
    }

    IEnumerator StartDialogue()
    {
        yield return new WaitUntil(() => _fadeInOut.GetImage().color.a > 0.99);
        yield return new WaitForSeconds(2);
        active = true;
        toScreen.DialogueWindowActive(true);
    }

    IEnumerator Restart()
    {
        _fadeInOut.FadeOut(1);
        yield return new WaitUntil(() => _fadeInOut.GetImage().color.a > 0.99);
        _fadeInOut.ShowText("LEVEL COMPLETED");
        yield return new WaitForSeconds(0.5f);
        if (GameSettings.GetPlayMode().Equals(GameConstants.levelMode)) { _sceneLoader.LoadStartScene(); }
        else if (GameSettings.GetPlayMode().Equals(GameConstants.historyMode)) { _sceneLoader.LoadTransitionScene(); }
        else { }
    }
}
