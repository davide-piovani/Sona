using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using ApplicationConstants;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;

public class Level3Manager : MonoBehaviour {

    private RoomLight[] lights;
    private Interactable[] int_obj;
    //public ActiveCharacterController character;
    Player currentPlayer;
    private Vector3[] positions = new Vector3[3] {new Vector3 (-40.5f, -0.65f, 0.2f), new Vector3 (-37f, -0.65f, -3f), new Vector3(14.49f, 0.2f, 28.539f)};
    public ComponentBox box;
    public GeneralSwitch gen;
    public Circuitry circ;
    public UIManager toScreen;
    //public SceneLoader loader;
    public WrittenPart lines;
    public Torch t;
    public Document doc;
    public Camera freeCam;
    public Text interactiveString;


    //public AudioListener outSound;
    private GameController controller;

    //used for internal functions
    private bool newMessage;
    private int state = 0;
    private bool waiting = false;
    private bool active = false;
    private FadeInOut _fade;

    // Use this for initialization
    void Start () {
        InitObjects();
        box.GetComponent<Transform>().position = positions[UnityEngine.Random.Range(0,2)];
        StartCoroutine(StartDialogue());
    }

    private void InitObjects() {
        int i;
        //outSound = FindObjectOfType<GlobalAudioListener>().GetComponent<AudioListener>();
        _fade = FindObjectOfType<FadeInOut>();
        lights = FindObjectsOfType<RoomLight>();
        controller = FindObjectOfType<GameController>();
        Switch[] switches = FindObjectsOfType <Switch>();
        toScreen.DialogueWindowActive(false);
        int_obj = new Interactable[switches.Length];
        System.Array.Copy(switches, int_obj, switches.Length);

        for (i=0; i<switches.Length; i++){
            switches[i].manager = this;
        }
        box.manager = this;
        gen.manager = this;
        circ.manager = this;
        doc.manager = this;
    }

    void Update () {
        if (active)
        {
            if (!waiting)
            {
                switch (state)
                {
                    case 0:
                        print("LEVEL MANAGER: State 0");
                        freeCam.gameObject.transform.position = new Vector3(-4.87f, 1.84f, -12.47f);
                        freeCam.gameObject.transform.rotation = Quaternion.Euler(30, 0, 0);
                        controller.DisableInput();
                        SetPlayer();
                        toScreen.PowerBarActive(false); /* TRY */
                        currentPlayer.Deactivate();
                        freeCam.enabled = true;
                        ShowDialogueLine();
                        state++;
                        break;
                    case 1:
                        print("LEVEL MANAGER: State 1");
                        for (int i = 0; i < lights.Length; i++)
                        {
                            lights[i].ShutDown();
                        }
                        interactiveString.color = new Vector4(1,1,1,1); /* change color string*/
                        state++;
                        break;
                    case 2:
                        print("LEVEL MANAGER: State 2");
                        ShowDialogueLine();
                        if (state == 3)
                        {
                            freeCam.enabled = false;
                            toScreen.PowerBarActive(true); /* TRY */
                            currentPlayer.Activate();
                            controller.ActiveInput();
                            ActivateTorch();
                        }
                        break;
                    case 3:
                        //print("LEVEL MANAGER: State 3");
                        if (!(controller.GetActivePlayer() == currentPlayer))
                        {
                            SetPlayer();
                        }

                        if (newMessage)
                        {
                            newMessage = false;
                        }
                        else
                        {
                            toScreen.EraseMessage();
                        }
                        break;
                    case 4:
                        print("LEVEL MANAGER: State 4");
                        freeCam.enabled = true;
                        freeCam.gameObject.transform.position = new Vector3(-14.1f, 1.84f, 22.5f);
                        freeCam.gameObject.transform.rotation = Quaternion.Euler(30, 0, 0);
                        controller.DisableInput();
                        SetPlayer();
                        currentPlayer.Deactivate();
                        ShowDocument();
                        break;
                    case 5:
                        print("LEVEL MANAGER: State 5");
                        EndLevel();
                        break;
                }
            }
            else
            {
                if (CrossPlatformInputManager.GetButtonDown(PlayersConstants.interactButton))
                {
                    toScreen.NextDial();
                }
            }
        }
    }

    private void SetPlayer(){
        int i;
        currentPlayer = controller.GetActivePlayer();
        for (i=0; i<int_obj.Length; i++){
            int_obj[i].player = currentPlayer.gameObject;
        }
        box.player = currentPlayer.gameObject;
        gen.player = currentPlayer.gameObject;
        circ.player = currentPlayer.gameObject;
        doc.player = currentPlayer.gameObject;
    }

    public void ActivateTorch() {
        Player[] players = controller.GetScenePlayers();
        for (int i=0; i<players.Length; i++){
            if (players[i].GetPlayerType() == PlayerType.Hannah){
                players[i].GetComponent<Animator>().SetBool("hasTorch", true);
                t.gameObject.SetActive(true);
                return;
            }
        }
    }

    public void RetrieveComponent(GameObject retriever){
        //print("LEVEL MANAGER: called for retrieve component");
        Destroy (box.gameObject);
        circ.SetRepairer(currentPlayer.gameObject);
    }

    public void ShutDown (){
        //print("LEVEL MANAGER: called for shut down");
        for(int i = 0; i < lights.Length; i++){
            lights[i].emergency = false;
            lights[i].ShutDown();
        }
        circ.shutDown = true;
    }

    public void RestorePower (){
        //print("LEVEL MANAGER: called for restore power");
        Switch s;
        for(int i = 0; i < lights.Length; i++){
            lights[i].Restore();
        }

        for(int i = 0; i < int_obj.Length; i++){
            s = int_obj[i] as Switch;
            if (s != null){
                s.power = true;
            }
        }

        interactiveString.color = new Vector4(0, 0, 0, 1); /* change color string*/
    }

    public void repair (){
        gen.repaired = true;
    }

    //Show message on the center of the screen
    public void ShowMessage (String message, int priority){
        newMessage = true;
        toScreen.ShowMessage (message, priority);
    }

    //Erase message from screen
    public void EraseMessage (){
        toScreen.EraseMessage();
    }

    public void ShowDoc(){
        state = 4;
    }

     private void ShowDialogueLine(){
        String name = lines.GetName();
        String line = lines.GetDialContent();

        if (name != null && line != null){
            toScreen.ShowDial(name, line);
            waiting = true;
        } else {
            state++;
        }
    }

    private void ShowDocument() {
        String title = lines.GetTitle();
        String content = lines.GetDocContent();

        if (title != null && content != null){
            toScreen.ShowDocument(title, content);
            waiting = true;
        } else {
            state++;
        }
    }

    public void EndLevel(){
        //Back to menu
        FindObjectOfType<EndAnim>().End();
        if (state < 6){
            state ++;
        }
    }

    public void Next () {
        waiting = false;
    }

    IEnumerator StartDialogue() {
        yield return new WaitUntil(() => _fade.GetImage().color.a > 0.99);
        yield return new WaitForSeconds(2);
        active = true;
        toScreen.DialogueWindowActive(true);
    }

}
