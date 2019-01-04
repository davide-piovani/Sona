using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;
using ApplicationConstants;
using System;

public class GameController : InputListener {

    [Header("General level info")]
    [SerializeField] AudioClip backgroundMusic;
    [SerializeField] PlayerType startingPlayer;

    [Header("UI")]
    [SerializeField] Image powerBar;
    [SerializeField] Image characterIcon;

    [Header("Input Listeners")]
    [SerializeField] InputListener pauseInterface;

    private Player activePlayer;

    private BackgroundAudioController audioController;
    private ActiveCharacterController characterController;
    private GameSlot gameSlot;

    // Use this for initialization
    void Start () {
        audioController = BackgroundAudioController.instance;
        characterController = FindObjectOfType<ActiveCharacterController>();
        LoadGameSlot();

        activePlayer = characterController.ActivePlayerOfType(startingPlayer);
        characterIcon.sprite = activePlayer.GetCharacterPortrait();

        ActiveInput();
        PlayBackgroundMusic();
    }

    private void LoadGameSlot() { 
        gameSlot = FindObjectOfType<SceneLoader>().GetGameSlot();
    }

    private void Update() {
        if (IsInputActive()){
            if (activePlayer != null && !activePlayer.IsInputActive()) activePlayer.ActiveInput();
            if (CrossPlatformInputManager.GetButtonDown(PlayersConstants.pauseButton)) PauseGame();
            if (CrossPlatformInputManager.GetButtonDown(PlayersConstants.changeCharacterButton)) ChangeCharacter();
        }
    }

    private void ChangeCharacter(){
        activePlayer = characterController.GiveControlToNextPlayer();
        characterIcon.sprite = activePlayer.GetCharacterPortrait();
    }

    private void PauseGame(){
        pauseInterface.gameObject.SetActive(true);
        if (activePlayer != null) activePlayer.DisableInput();
        pauseInterface.SetAsUniqueInputListener(this);
    }

    public void UpdatePowerLevelIndicator(float level) {
        powerBar.fillAmount = level;
    }

    public Player[] GetScenePlayers() { return characterController.GetScenePlayers(); }

    private void PlayBackgroundMusic(){
        audioController.SetVolume(gameSlot.musicVolume);
        audioController.PlayBackgroundMusic(backgroundMusic);
    }

    public Player GetActivePlayer() { return activePlayer; }

    public float GetEffectsVolume(){
        return gameSlot.effectsVolume;
    }
}