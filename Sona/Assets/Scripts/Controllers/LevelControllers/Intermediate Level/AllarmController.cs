using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllarmController : MonoBehaviour {

    public float velocity1 = 50f;
    float timeInterval = 5f;
    float interval;
    int shot = 0;
    bool active = false;
    AllarmRow[] _rows;

    void Start()
    {
        _rows = GetComponentsInChildren<AllarmRow>();
        interval = timeInterval;
        foreach (AllarmRow r in _rows)
        {
            r.Shot(shot);
        }
    }

    void Update()
    {

        if (interval >= 0)
        {

            interval -= velocity1 * TimeController.GetDelTaTime();
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

}
