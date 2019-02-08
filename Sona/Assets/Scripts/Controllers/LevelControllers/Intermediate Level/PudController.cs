using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PudController : MonoBehaviour {

    SceneLoader _sceneLoader;
    FadeInOut _fadeInOut;
    
    void Start()
    {
        _sceneLoader = FindObjectOfType<SceneLoader>();
        _fadeInOut = FindObjectOfType<FadeInOut>();
    }

    void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Restart());
        }
    }

    IEnumerator Restart() {
        _fadeInOut.FadeOut(5);
        yield return new WaitUntil(() => _fadeInOut.GetImage().color.a > 0.99);
        _fadeInOut.ShowText("YOU'RE DEAD");
        yield return new WaitForSeconds(0.5f);
        _sceneLoader.ReloadCurrentScene();
    }

}
