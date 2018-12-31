using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;
using ApplicationConstants;
using System;

public class GameController : InputListener {

    [SerializeField] Transform LoadingBar;
    [SerializeField] AudioClip backgroundMusic;
    [SerializeField] PlayerType startingPlayer;

    [Header("Input Listeners")]
    [SerializeField] InputListener pauseInterface;
    private Player activePlayer;
    private GameObject powerBar;
    private Image characterPortrait;
    private BackgroundAudioController audioController;
    private ActiveCharacterController characterController;

    // Use this for initialization
    void Start () {
        audioController = BackgroundAudioController.instance;
        characterController = FindObjectOfType<ActiveCharacterController>();

        activePlayer = characterController.ActivePlayerOfType(startingPlayer);

        powerBar = GameObject.Find("PowerBar");
        ActiveInput();
        PlayBackgroundMusic();
    }

    private void Update() {
        if (IsInputActive()){
            if (activePlayer != null && !activePlayer.IsInputActive())
            {
                activePlayer.ActiveInput();
                //characterPortrait = characterController.GetComponentInChildren<Image>();
            }
            if (CrossPlatformInputManager.GetButtonDown(PlayersConstants.pauseButton)) PauseGame();
            if (CrossPlatformInputManager.GetButtonDown(PlayersConstants.changeCharacterButton)) ChangeCharacter();
        }
    }

    private void ChangeCharacter(){
        activePlayer = characterController.GiveControlToNextPlayer();
    }

    private void PauseGame(){
        pauseInterface.gameObject.SetActive(true);
        if (activePlayer != null) activePlayer.DisableInput();
        pauseInterface.SetAsUniqueInputListener(this);
    }

    public void UpdatePowerLevelIndicator(float level) {
        LoadingBar.GetComponent<Image>().fillAmount = level;
    }

    public Player[] GetScenePlayers() { return characterController.GetScenePlayers(); }

    private void PlayBackgroundMusic(){
        audioController.PlayBackgroundMusic(backgroundMusic);
    }

    public Player GetActivePlayer() { return activePlayer; }
}