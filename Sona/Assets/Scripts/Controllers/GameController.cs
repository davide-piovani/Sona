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

    private Light[] sceneLights;
    private AlarmLight[] alarms;
    private bool alarmActive = false;
    private float alarmTime = 0;

    private Player activePlayer;
    private SceneLoader sceneLoader;

    private BackgroundAudioController audioController;
    private ActiveCharacterController characterController;

    bool pauseActive = true;
    bool changePlayerActive = true;

    public bool lastDoorOpen = false;

    // Use this for initialization
    void Start () {
        audioController = BackgroundAudioController.instance;
        characterController = FindObjectOfType<ActiveCharacterController>();
        sceneLoader = FindObjectOfType<SceneLoader>();
        sceneLights = FindObjectsOfType<Light>();
        alarms = FindObjectsOfType<AlarmLight>();

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
            if (activePlayer != null && !activePlayer.IsInputActive()){
                activePlayer.ActiveInput();
                activePlayer.GetComponentInChildren<CameraController>().ActiveInput();
            }
            if (pauseActive & CrossPlatformInputManager.GetButtonDown(PlayersConstants.pauseButton)) PauseGame();
            if (changePlayerActive & CrossPlatformInputManager.GetButtonDown(PlayersConstants.changeCharacterButton)) ChangeCharacter();
        }
        if (alarmActive) ManageAlarms();
    }

    private void ChangeCharacter(){
        activePlayer = characterController.GiveControlToNextPlayer();
        characterIcon.sprite = activePlayer.GetCharacterPortrait();
    }

    private void PauseGame(){
        //print ("Start pause");
        pauseInterface.gameObject.SetActive(true);
        if (activePlayer != null) {
            activePlayer.DisableInput();
            activePlayer.gameObject.GetComponentInChildren<CameraController>().DisableInput();
        }
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
                gameSlot.JackPosX = player.transform.position.x;
                gameSlot.JackPosY = player.transform.position.y;
                gameSlot.JackPosZ = player.transform.position.z;
                gameSlot.JackRotX = player.transform.rotation.x;
                gameSlot.JackRotY = player.transform.rotation.y;
                gameSlot.JackRotZ = player.transform.rotation.z;
                gameSlot.JackRotW = player.transform.rotation.w;
                break;
            case PlayerType.Hannah:
                gameSlot.HannahPosX = player.transform.position.x;
                gameSlot.HannahPosY = player.transform.position.y;
                gameSlot.HannahPosZ = player.transform.position.z;
                gameSlot.HannahRotX = player.transform.rotation.x;
                gameSlot.HannahRotY = player.transform.rotation.y;
                gameSlot.HannahRotZ = player.transform.rotation.z;
                gameSlot.HannahRotW = player.transform.rotation.w;
                break;
            case PlayerType.Charlie:
                gameSlot.CharliePosX = player.transform.position.x;
                gameSlot.CharliePosY = player.transform.position.y;
                gameSlot.CharliePosZ = player.transform.position.z;
                gameSlot.CharlieRotX = player.transform.rotation.x;
                gameSlot.CharlieRotY = player.transform.rotation.y;
                gameSlot.CharlieRotZ = player.transform.rotation.z;
                gameSlot.CharlieRotW = player.transform.rotation.w;
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
                player.transform.position = new Vector3(gameSlot.JackPosX, gameSlot.JackPosY, gameSlot.JackPosZ);
                player.transform.rotation = new Quaternion(gameSlot.JackRotX, gameSlot.JackRotY, gameSlot.JackRotZ, gameSlot.JackRotW);
                break;
            case PlayerType.Hannah:
                player.transform.position = new Vector3(gameSlot.HannahPosX, gameSlot.HannahPosY, gameSlot.HannahPosZ);
                player.transform.rotation = new Quaternion(gameSlot.HannahRotX, gameSlot.HannahRotY, gameSlot.HannahRotZ, gameSlot.HannahRotW);
                break;
            case PlayerType.Charlie:
                player.transform.position = new Vector3(gameSlot.CharliePosX, gameSlot.CharliePosY, gameSlot.CharliePosZ);
                player.transform.rotation = new Quaternion(gameSlot.CharlieRotX, gameSlot.CharlieRotY, gameSlot.CharlieRotZ, gameSlot.CharlieRotW);
                break;
        }
    }

    public void PlayerCatched(){
        ReloadFromLastCheckpoint();
    }

    public void SetAlarm(bool active) { alarmActive = active; }
    public void ChangeAlarmState(bool active){
        foreach(AlarmLight alarm in alarms){
            alarm.ChangeState(active);
        }
    }

    private void ManageAlarms() {
        alarmTime += TimeController.GetDelTaTime();
        if (alarmTime >= GameConstants.alarmSwitchTime){
            alarmTime = 0;
            ToggleAlarmLights();
        }
    }

    private void ToggleAlarmLights(){
        foreach(AlarmLight alarm in alarms){
            alarm.ToggleLights();
        }
    }

    public void ToggleSceneLights(bool active){
        foreach(Light sceneLight in sceneLights){
            sceneLight.enabled = active;
        }
    }

    /*private void Follow() {
        Player[] allPlayers;

        allPlayers = characterController.GetScenePlayer();
    }*/
}