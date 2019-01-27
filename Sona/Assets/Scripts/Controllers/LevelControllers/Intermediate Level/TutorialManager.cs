using ApplicationConstants;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class TutorialManager : MonoBehaviour {

    Player currentPlayer;
    Vector3[] positions = new Vector3[3] { new Vector3(-40.5f, -0.65f, 0.2f), new Vector3(-37f, -0.65f, -3f), new Vector3(14.49f, 0.2f, 28.539f) };
    public CanvasManager toScreen;
    public TutorialTexts lines;
    public Camera freeCam;

    private GameController controller;

    //used for internal functions
    bool newMessage;
    int state = 0;
    bool waiting = false;
    bool active = false;
    FadeInOut _fade;

    // Use this for initialization
    void Start()
    {
        freeCam.enabled = false;
        InitObjects();
        StartCoroutine(StartDialogue());
    }

    private void InitObjects()
    {
        _fade = FindObjectOfType<FadeInOut>();
        controller = FindObjectOfType<GameController>();
        toScreen.DialogueWindowActive(false);
        toScreen.MSGEnabled(false);
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
                        print("TUTORIAL MANAGER: State 0");
                        //freeCam.gameObject.transform.position = new Vector3(-4.87f, 1.84f, -12.47f);
                        //freeCam.gameObject.transform.rotation = Quaternion.Euler(30, 0, 0);
                        controller.DisableInput();
                        SetPlayer();
                        toScreen.PowerBarActive(false);
                        currentPlayer.Deactivate();
                        freeCam.enabled = true;
                        ShowDialogueLine();
                        if (state == 1)
                        {
                            freeCam.enabled = false;
                            toScreen.PowerBarActive(true);
                            currentPlayer.Activate();
                            controller.ActiveInput();
                        }
                        break;
                    case 1:
                        print("TUTORIAL MANAGER: State 1");
                        toScreen.MSGEnabled(true);
                        toScreen.ShowMessage("Press N to change character until Charlie is selected");
                        if (controller.GetActivePlayer().name.Equals("Charlie")) { state++; }
                        break;
                    case 2:
                        print("TUTORIAL MANAGER: State 2");
                        toScreen.ShowMessage("Press Z to activate power");
                        if (CrossPlatformInputManager.GetButtonDown(PlayersConstants.powerButtonName))
                        {
                            toScreen.EraseMessage();
                            toScreen.MSGEnabled(false);
                            state++;
                        }
                        break;
                    case 3:
                        print("TUTORIAL MANAGER: State 3");
                        //gioca
                        break;
                    case 4:
                        print("TUTORIAL MANAGER: State 4");
                        /*freeCam.enabled = true;
                        freeCam.gameObject.transform.position = new Vector3(-14.1f, 1.84f, 22.5f);
                        freeCam.gameObject.transform.rotation = Quaternion.Euler(30, 0, 0);
                        controller.DisableInput();
                        SetPlayer();
                        currentPlayer.Deactivate();
                        ShowDocument();*/
                        break;
                    case 5:
                        print("TUTORIAL MANAGER: State 5");
                        //EndLevel();
                        break;
                }
            }
            else
            {
                if (CrossPlatformInputManager.GetButtonDown(PlayersConstants.enterButton))
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

    private void ShowDialogueLine()
    {
        string name = lines.GetName();
        string line = lines.GetDialContent();

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

    /*public void EndLevel()
    {
        //Back to menu
        FindObjectOfType<EndAnim>().End();
        if (state < 6)
        {
            state++;
        }
    }*/

    public void Next()
    {
        waiting = false;
    }

    IEnumerator StartDialogue()
    {
        yield return new WaitUntil(() => _fade.GetImage().color.a > 0.99);
        yield return new WaitForSeconds(2);
        active = true;
        toScreen.DialogueWindowActive(true);
    }
}
