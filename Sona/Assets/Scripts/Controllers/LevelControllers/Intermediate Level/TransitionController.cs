using ApplicationConstants;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransitionController : MonoBehaviour {

    public Text txt;
    SceneLoader sceneLoader;

    void Start () {
        sceneLoader = FindObjectOfType<SceneLoader>();
        if (GameSettings.GetCurrentSceneNumber() == 1) { txt.text = "LOADING LEVEL 1"; }
        else if (GameSettings.GetCurrentSceneNumber() == 2) { txt.text = "LOADING LEVEL 2"; }
        else if (GameSettings.GetCurrentSceneNumber() == 3) { txt.text = "LOADING PART 2"; }
        else if (GameSettings.GetCurrentSceneNumber() == 4) { txt.text = "LOADING LEVEL 3"; }
        else { txt.text = "CONGRATULATIONS! YOU HAVE FINISHED THE GAME"; }
        StartCoroutine("LaunchLevel");
    }

    IEnumerator LaunchLevel()
    {
        yield return new WaitForSeconds(1f);
        sceneLoader.LoadScene(GameSettings.GetCurrentSceneNumber()+1);
     }
}
