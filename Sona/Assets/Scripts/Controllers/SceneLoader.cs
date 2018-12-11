using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {

    private void Awake(){
        var sceneLoaders = FindObjectsOfType<SceneLoader>();
        if (sceneLoaders.Length > 1) Destroy(gameObject);
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

    public void LoadScene(string scene){
        SceneManager.LoadScene(scene);
    }

    public void QuitGame(){
        Application.Quit();
    }
}
