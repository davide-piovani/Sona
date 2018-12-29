using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;
using ApplicationConstants;

public class GameController : InputListener {

    [SerializeField] Transform LoadingBar;
    [SerializeField] Player[] scenePlayers;
    [SerializeField] AudioClip backgroundMusic;

    [Header("Input Listeners")]
    [SerializeField] InputListener pauseInterface;
    [SerializeField] InputListener activePlayer;

    private BackgroundAudioController audioController;

    // Use this for initialization
    void Start () {
        audioController = BackgroundAudioController.instance;
        ActiveInput();
        PlayBackgroundMusic();
    }

    private void Update() {
        if (IsInputActive()){
            if (activePlayer != null && !activePlayer.IsInputActive()) activePlayer.ActiveInput();
            if (CrossPlatformInputManager.GetButtonDown(PlayersConstants.pauseButton)) PauseGame();
        }
    }

    private void PauseGame(){
        pauseInterface.gameObject.SetActive(true);
        if (activePlayer != null) activePlayer.DisableInput();
        pauseInterface.SetAsUniqueInputListener(this);
    }

    public void UpdatePowerLevelIndicator(float level) {
        LoadingBar.GetComponent<Image>().fillAmount = level;
    }

    public Player[] GetScenePlayers() { return scenePlayers; }

    private void PlayBackgroundMusic(){
        audioController.PlayBackgroundMusic(backgroundMusic);
    }
}