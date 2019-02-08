using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using ApplicationConstants;

public class PauseController : MenuMovementController {

    [SerializeField] SettingsController settingsController;
    SceneLoader _sceneLoader;
    GameController _gameController;

    private void Start(){
        _sceneLoader = FindObjectOfType<SceneLoader>();
        _gameController = FindObjectOfType<GameController>();
        InitializeMenu();
    }

    void Update () {
		if (IsInputActive()) {
            CheckResumeButton();
            CheckVerticalInput();
            CheckEnterButton();
        }
    }

    private void CheckResumeButton() {
        if (GameSettings.GetButtonDown(PlayersConstants.pauseButton)) Resume();
    }

    private void CheckEnterButton(){
        if (GameSettings.GetButtonDown(PlayersConstants.enterButton)) {
            AudioEffects.PlaySound(AudioEffects.instance.menuButtonClicked);
            switch (currentButton){
                case 0:
                    Resume();
                    break;
                case 1:
                    RestartLevel();
                    break;
                case 2:
                    ShowSettings();
                    break;
                case 3:
                    Save();
                    break;
                case 4:
                    ReturnToMainMenu();
                    break;
            }
        }
    }

    private void Resume(){
        RestoreOldListener();
        _gameController.ManagePowerActive(true);
        _gameController.ChangePlayerActive(true);
        _gameController.GamePaused(false);
        gameObject.SetActive(false);
    }

    private void RestartLevel(){
        _gameController.RestartLevel();
    }

    private void ShowSettings(){
        for (int i = 0; i < gameObject.transform.childCount; i++){
            gameObject.transform.GetChild(i).gameObject.SetActive(false);
        }
        settingsController.gameObject.SetActive(true);
        settingsController.InizializeButtons();
        settingsController.SetAsUniqueInputListener(this);
    }

    private void Save() {
        _sceneLoader.GetGameSlot().Save();
    }

    private void ReturnToMainMenu() {
        _sceneLoader.LoadStartScene();
    }

    public void RestorePauseMenu() {
        settingsController.gameObject.SetActive(false);
        for (int i = 0; i < gameObject.transform.childCount; i++){
            gameObject.transform.GetChild(i).gameObject.SetActive(true);
        }
    }
}