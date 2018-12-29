using UnityEngine;
using UnityEngine.SceneManagement;
using ApplicationConstants;

public class SceneLoader : MonoBehaviour {

    public GameSlot gameSlot;

    private void Awake(){
        var sceneLoaders = FindObjectsOfType<SceneLoader>();
        if (sceneLoaders.Length > 1) {
            Destroy(gameObject);
        } else {
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public void LoadNextScene(){
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
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
        SceneManager.LoadScene(0);
    }

    public void LoadScene(int scene){
        SceneManager.LoadScene(scene);
    }

    public void LoadScene(string scene){
        SceneManager.LoadScene(scene);
    }

    public void LoadScene(SceneType sceneType){
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
            default: return SceneNames.menu;
        }
    }
}
