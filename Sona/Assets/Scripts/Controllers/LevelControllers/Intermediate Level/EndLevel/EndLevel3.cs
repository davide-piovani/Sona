using ApplicationConstants;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevel3 : MonoBehaviour {

    bool jack = false;
    bool hannah = false;
    bool charlie = false;
    SceneLoader _sceneLoader;
    FadeInOut _fadeInOut;

    void Start()
    {
        _sceneLoader = FindObjectOfType<SceneLoader>();
        _fadeInOut = FindObjectOfType<FadeInOut>();
    }

    void Update()
    {
        if (jack & hannah & charlie)
        {
            StartCoroutine(LevelEnd());
        }
    }

    void OnTriggerEnter(Collider other)
    {
        /*if (other.CompareTag("Player")) {
            StartCoroutine(LevelEnd());
        }*/

        if (other.CompareTag("Player"))
        {
            if (other.gameObject.name.Equals("Jack")) { jack = true; }
            else if (other.gameObject.name.Equals("Hannah")) { hannah = true; }
            else if (other.gameObject.name.Equals("Charlie")) { charlie = true; }
            else { }
        }

    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.gameObject.name.Equals("Jack")) { jack = false; }
            else if (other.gameObject.name.Equals("Hannah")) { hannah = false; }
            else if (other.gameObject.name.Equals("Charlie")) { charlie = false; }
            else { }
        }
    }

    IEnumerator LevelEnd()
    {
        _fadeInOut.FadeOut(1);
        yield return new WaitUntil(() => _fadeInOut.GetImage().color.a > 0.99);
        _fadeInOut.ShowText("LEVEL COMPLETED");
        yield return new WaitForSeconds(2);
        if (GameSettings.GetPlayMode().Equals(GameConstants.levelMode)) { _sceneLoader.LoadStartScene(); }
        else if (GameSettings.GetPlayMode().Equals(GameConstants.historyMode)) { _sceneLoader.LoadTransitionScene(); }
        else { }
    }
}
