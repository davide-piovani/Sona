using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombinationScheme : MonoBehaviour {

    float timeInterval = 5f;
    float interval1;
    float velocity1 = 200;
    int shot1;
    SquareInterface[] _squares;
    
    void Start()
    {
        _squares = GetComponentsInChildren<SquareInterface>();
        interval1 = timeInterval;
        shot1 = 0;
        foreach (SquareInterface r in _squares)
        {
            r.Shot(shot1);
        }
    }

    void Update()
    {

        if (interval1 >= 0)
        {

            interval1 -= velocity1 * TimeController.GetDelTaTime();
        }

        if (interval1 <= 0)
        {

            if (shot1 == 8)
            {
                shot1 = 0;
            }
            else
            {
                shot1++;
            }

            foreach (SquareInterface r in _squares)
            {
                r.Shot(shot1);
            }
            interval1 = timeInterval;
        }

    }
}
