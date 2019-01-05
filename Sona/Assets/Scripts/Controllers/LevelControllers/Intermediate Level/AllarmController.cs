using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllarmController : MonoBehaviour {

    public float velocity = 50f;
    public float timeInterval = 5f;
    public float velocityRing = 0.5f;
    public float intervalRing = 500f;
    [Range(0,5)]
    public int endShot = 5;
    float interval;
    int shot = 0;
    bool active = false;
    bool enemyDetect = false;
    bool allarmActive = false;
    AllarmRow[] _rows;

    SceneLoader _sceneLoader;
    FadeInOut _fadeInOut;


    void Start()
    {
        _sceneLoader = FindObjectOfType<SceneLoader>();
        _fadeInOut = FindObjectOfType<FadeInOut>();
        _rows = GetComponentsInChildren<AllarmRow>();
        interval = timeInterval;
        foreach (AllarmRow r in _rows)
        {
            r.Shot(shot);
        }
    }

    void Update()
    {

        if (!enemyDetect)
        {
            if (interval >= 0)
            {
                interval -= velocity * TimeController.GetDelTaTime();
            }

            if (interval <= 0)
            {
                if (shot == 5)
                {
                    shot = 0;
                }
                else
                {
                    shot++;
                }

                foreach (AllarmRow r in _rows)
                {
                    r.Shot(shot);
                }
                interval = timeInterval;
            }
        }
        else if (enemyDetect & !allarmActive)
        {
            ActiveAllarm();
        }
        else if (enemyDetect & allarmActive)
        {

            if (interval >= 0)
            {
                interval -= velocityRing * TimeController.GetDelTaTime();
            }

            if (interval <= 0)
            {
                if (shot == endShot)
                {
                    StartCoroutine(Restart());
                }
                else
                {
                    shot++;
                }

                foreach (AllarmRow r in _rows)
                {
                    r.Shot(shot);
                }
                interval = intervalRing;
            }
        }
        else { }

    }

    public void EnemyDetected() {
        enemyDetect = true;
    }

    public bool IsEnemyDetect() {
        return enemyDetect;
    }

    void ActiveAllarm() {
        foreach (AllarmRow r in _rows) {
            r.ResetSquares();
        }
        interval = intervalRing;
        shot = 0;
        foreach (AllarmRow r in _rows)
        {
            r.Shot(shot);
        }
        allarmActive = true;
    }

    IEnumerator Restart()
    {
        _fadeInOut.FadeOut();
        yield return new WaitUntil(() => _fadeInOut.GetImage().color.a == 1);
        _fadeInOut.ShowText("CAPTURED");
        yield return new WaitForSeconds(2);
        _sceneLoader.ReloadCurrentScene();
    }

}
