using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using ApplicationConstants;

public class PauseController : MenuMovementController {

    [SerializeField] SettingsController settingsController;

    private void Start(){
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
        if (CrossPlatformInputManager.GetButtonDown(PlayersConstants.pauseButton)) Resume();
    }

    private void CheckEnterButton(){
        if (CrossPlatformInputManager.GetButtonDown(PlayersConstants.enterButton)){
            switch (currentButton){
                case 0:
                    Resume();
                    break;
                case 1:
                    RestartFromLastCheckpoint();
                    break;
                case 2:
                    ShowSettings();
                    break;
                case 3:
                    ReturnToMainMenu();
                    break;
            }
        }
    }

    private void Resume(){
        RestoreOldListener();
        gameObject.SetActive(false);
    }

    private void RestartFromLastCheckpoint(){
        print("RestartFromLastCheckpoint to be implemented");
    }

    private void ShowSettings(){
        for(int i = 0; i < gameObject.transform.childCount; i++){
            gameObject.transform.GetChild(i).gameObject.SetActive(false);
        }
        settingsController.gameObject.SetActive(true);
        settingsController.SetAsUniqueInputListener(this);
    }

    private void ReturnToMainMenu() {
        FindObjectOfType<SceneLoader>().LoadStartScene();
    }
}