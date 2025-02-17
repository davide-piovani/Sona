﻿using UnityEngine;
using UnityEngine.SceneManagement;
using ApplicationConstants;

public class SceneLoader : MonoBehaviour {

    private GameSlot gameSlot;

    private void Awake(){
        var sceneLoaders = FindObjectsOfType<SceneLoader>();
        if (sceneLoaders.Length > 1) {
            Destroy(gameObject);
        } else {
            DontDestroyOnLoad(this.gameObject);
            gameSlot = SaveSystem.LoadGameSlot(0);
        }
    }

    public GameSlot GetGameSlot() { return gameSlot; }
    public void SetGameSlot(GameSlot slot) { gameSlot = slot; }

    public void LoadNextScene(){
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (gameSlot != null) gameSlot.shouldRestorePos = false;
        if (currentSceneIndex == SceneManager.sceneCount-1) {
            LoadStartScene();
        } else {
            SceneManager.LoadScene(currentSceneIndex + 1);
        }
    }

    public void ReloadCurrentScene() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadStartScene(){
        if (gameSlot != null) gameSlot.shouldRestorePos = false;
        GameSettings.SetCurrentSceneNumber(1);
        SceneManager.LoadScene(1);
    }

    public void LoadTransitionScene()
    {
        if (gameSlot != null) gameSlot.shouldRestorePos = false;
        SceneManager.LoadScene(7);
    }

    public void LoadScene(int scene){
        if (gameSlot != null) gameSlot.shouldRestorePos = false;
        GameSettings.SetCurrentSceneNumber(scene);
        SceneManager.LoadScene(scene);
    }

    public void LoadScene(string scene){
        if (gameSlot != null) gameSlot.shouldRestorePos = false;
        GameSettings.SetCurrentSceneNumber(SceneManager.GetSceneByName(scene).buildIndex);
        SceneManager.LoadScene(scene);
    }

    public void LoadScene(SceneType sceneType){
        GameSettings.SetCurrentSceneNumber(SceneManager.GetSceneByName(GetSceneName(sceneType)).buildIndex);
        LoadScene(GetSceneName(sceneType));
    }

    public int GetSceneIndex() { return SceneManager.GetActiveScene().buildIndex; }

    public void QuitGame(){
        Application.Quit();
    }

    private string GetSceneName(SceneType sceneType){
        switch (sceneType){
            case SceneType.mainMenu: return SceneNames.menu;
            case SceneType.Level_1: return SceneNames.level1;
            case SceneType.Level_2: return SceneNames.level2;
            case SceneType.Level_3: return SceneNames.level3;
            case SceneType.transition: return SceneNames.transition;
            default: return SceneNames.menu;
        }
    }
}
