using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevel2_1 : MonoBehaviour {

    SceneLoader _sceneLoader;
    FadeInOut _fadeInOut;

    bool sensorR_active = false;
    bool sensorL_active = false;

    void Start()
    {
        _sceneLoader = FindObjectOfType<SceneLoader>();
        _fadeInOut = FindObjectOfType<FadeInOut>();
    }

    void Update () {
        if (sensorR_active & sensorL_active) {
            StartCoroutine(LevelEnd());
        }
	}

    public void SensorR_Active(bool cond) {
        sensorR_active = cond;
    }

    public void SensorL_Active(bool cond) {
        sensorL_active = cond;
    }

    IEnumerator LevelEnd()
    {
        _fadeInOut.FadeOut(1);
        yield return new WaitUntil(() => _fadeInOut.GetImage().color.a > 0.99);
        _fadeInOut.ShowText("FIRST PART COMPLETED");
        yield return new WaitForSeconds(2);
        _sceneLoader.LoadTransitionScene();
    }
}
