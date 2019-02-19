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
        _text.text = "Davide Piovani";
        yield return new WaitForSeconds(2);
        _text.text = "Federico Reale";
        yield return new WaitForSeconds(2);
        _text.text = "Luca Sartor";
        yield return new WaitForSeconds(2);
        _text.text = "Gabriele Iannone";
        yield return new WaitForSeconds(2);
        _text.text = "Leonardo Codamo";
        yield return new WaitForSeconds(2);
        _text.text = "Developed for the \nVideo game Design and Programming course \nof the Politecnico di Milano";
        yield return new WaitForSeconds(2);

        _sceneLoader.LoadStartScene();
    }
}
