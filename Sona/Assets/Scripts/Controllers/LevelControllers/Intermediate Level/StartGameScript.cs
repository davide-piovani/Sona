using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartGameScript : MonoBehaviour {

    public Image _img1;
    public Text _txt1;
    public Text _txt2;
    public Text _txt3;
    SceneLoader _sceneLoader;

    void Start() {
        _sceneLoader = FindObjectOfType<SceneLoader>();
        _img1.gameObject.SetActive(false);
        _txt1.gameObject.SetActive(false);
        _txt2.gameObject.SetActive(false);
        _txt3.gameObject.SetActive(false);
        StartCoroutine("Begin");
    }

    IEnumerator Begin() {
        yield return new WaitForSeconds(1.5f);
        _img1.gameObject.SetActive(true);
        yield return new WaitForSeconds(3);
        _img1.gameObject.SetActive(false);
        _txt1.gameObject.SetActive(true);
        yield return new WaitForSeconds(3);
        _txt1.gameObject.SetActive(false);
        _txt2.gameObject.SetActive(true);
        yield return new WaitForSeconds(3);
        _txt2.gameObject.SetActive(false);
        _txt3.gameObject.SetActive(true);
        yield return new WaitForSeconds(3);

        _sceneLoader.LoadStartScene();
    }
}
