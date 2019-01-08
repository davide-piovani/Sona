using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PudController : MonoBehaviour {

    SceneLoader _sceneLoader;
    FadeInOut _fadeInOut;
    Animator _animatorController;
    float deltaTime;
    
    void Start()
    {
        _sceneLoader = FindObjectOfType<SceneLoader>();
        _fadeInOut = FindObjectOfType<FadeInOut>();
        _animatorController = GetComponent<Animator>();
    }

    void Update()
    {

        deltaTime = TimeController.GetDelTaTime();
        _animatorController.SetFloat("speed", deltaTime);

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
        FindObjectOfType<GameController>().RestartLevel();
    }

}
