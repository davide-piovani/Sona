using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditsScript : MonoBehaviour {

    Text _text;
    SceneLoader _sceneLoader;
    
    void Start()
    {
        _text = GetComponentInChildren<Text>();
        _text.text = "CREDITS";
        _sceneLoader = FindObjectOfType<SceneLoader>();
        StartCoroutine(LevelEnd());
    }

    IEnumerator LevelEnd()
    {
        
        yield return new WaitForSeconds(2);
        _text.text = "AAA";
        yield return new WaitForSeconds(2);
        _text.text = "BBB";
        yield return new WaitForSeconds(2);
        _text.text = "CCC";
        yield return new WaitForSeconds(2);
        _sceneLoader.LoadStartScene();
    }
}
