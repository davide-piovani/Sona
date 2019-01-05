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
    private SceneLoader sceneLoader;

    private BackgroundAudioController audioController;
    private ActiveCharacterController characterController;

    bool pauseActive = true;
    bool changePlayerActive = true;

    // Use this for initialization
    void Start () {
        audioController = BackgroundAudioController.instance;
        characterController = FindObjectOfType<ActiveCharacterController>();
        sceneLoader = FindObjectOfType<SceneLoader>();

        characterController.DeactiveAll();
        activePlayer = characterController.ActivePlayerOfType(startingPlayer);
        characterIcon.sprite = activePlayer.GetCharacterPortrait();

        ActiveInput();
        PlayBackgroundMusic();

        OnLevelLoaded();
    }

    void OnLevelLoaded(){
        GameSlot gameSlot = GetCurrentGameSlot();

        if (gameSlot.shouldRestorePos){
            Player[] players = GetScenePlayers();

            foreach (Player player in players){
                RestorePlayerPos(player, gameSlot);
            }

            activePlayer = characterController.ActivePlayerOfType(gameSlot.activePlayer);
            characterIcon.sprite = activePlayer.GetCharacterPortrait();
        }
    }

    private void Update() {
        if (IsInputActive()){
            if (activePlayer != null && !activePlayer.IsInputActive()) activePlayer.ActiveInput();
            if (pauseActive & CrossPlatformInputManager.GetButtonDown(PlayersConstants.pauseButton)) PauseGame();
            if (changePlayerActive & CrossPlatformInputManager.GetButtonDown(PlayersConstants.changeCharacterButton)) ChangeCharacter();
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

    public Player GetActivePlayer() { return activePlayer; }
    public Player[] GetScenePlayers() { return characterController.GetScenePlayers(); }

    private void PlayBackgroundMusic(){
        audioController.SetVolume(GetCurrentGameSlot().musicVolume);
        audioController.PlayBackgroundMusic(backgroundMusic);
    }

    public float GetEffectsVolume(){
        return GetCurrentGameSlot().effectsVolume;
    }

    private GameSlot GetCurrentGameSlot(){
        return sceneLoader.GetGameSlot();
    }

    public void PauseActive(bool cond) {
        pauseActive = cond;
    }

    public void ChangePlayerActive(bool cond) {
        changePlayerActive = cond;
    }

    public void CheckpointReached(){
        GameSlot gameSlot = GetCurrentGameSlot();
        Player[] players = GetScenePlayers();

        gameSlot.shouldRestorePos = true;
        gameSlot.activePlayer = activePlayer.GetPlayerType();

        foreach(Player player in players){
            SavePlayerPos(player, gameSlot);
        }
    }

    private void SavePlayerPos(Player player, GameSlot gameSlot){
        switch (player.GetPlayerType()){
            case PlayerType.Jack:
                gameSlot.JackPos = player.transform.position;
                gameSlot.JackRotation = player.transform.rotation;
                break;
            case PlayerType.Hannah:
                gameSlot.HannahPos = player.transform.position;
                gameSlot.HannahRotation = player.transform.rotation;
                break;
            case PlayerType.Charlie:
                gameSlot.CharliePos = player.transform.position;
                gameSlot.CharlieRotation = player.transform.rotation;
                break;
        }
    }

    public void ReloadFromLastCheckpoint(){
        sceneLoader.ReloadCurrentScene();
    }

    public void RestartLevel(){
        GetCurrentGameSlot().shouldRestorePos = false;
        sceneLoader.ReloadCurrentScene();
    }

    private void RestorePlayerPos(Player player, GameSlot gameSlot){
        switch (player.GetPlayerType()){
            case PlayerType.Jack:
                player.transform.position = gameSlot.JackPos;
                player.transform.rotation = gameSlot.JackRotation;
                break;
            case PlayerType.Hannah:
                player.transform.position = gameSlot.HannahPos;
                player.transform.rotation = gameSlot.HannahRotation;
                break;
            case PlayerType.Charlie:
                player.transform.position = gameSlot.CharliePos;
                player.transform.rotation = gameSlot.CharlieRotation;
                break;
        }
    }

    public void PlayerCatched(){
        ReloadFromLastCheckpoint();
    }

}