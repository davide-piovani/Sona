using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour {

    SceneLoader _sceneLoader;
    FadeInOut _fadeInOut;

    void Start () {
        _sceneLoader = FindObjectOfType<SceneLoader>();
        _fadeInOut = FindObjectOfType<FadeInOut>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) {
            StartCoroutine(Restart());
        }
    }

    IEnumerator Restart()
    {
        _fadeInOut.FadeOut(1);
        yield return new WaitUntil(() => _fadeInOut.GetImage().color.a > 0.99);
        _fadeInOut.ShowText("LEVEL COMPLETED");
        yield return new WaitForSeconds(2);
        _sceneLoader.ReloadCurrentScene();
        //FindObjectOfType<GameController>().RestartLevel();
        // da cambiare e mettere passa al livello successivo
    }
}
