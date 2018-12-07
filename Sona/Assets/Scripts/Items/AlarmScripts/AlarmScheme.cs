using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmScheme : MonoBehaviour {

    public GameObject checkPoint;
    public float velocity1 = 50f;
    public float velocity2 = 200f;
    float timeInterval = 5f;
    float interval1;
    float interval2;
    int shot1;
    int shot2;
    bool active;
    RowInterfaceP1[] _rows1;
    RowInterfaceP2[] _rows2;
    
    void Start()
    {
        _rows1 = GetComponentsInChildren<RowInterfaceP1>();
        _rows2 = GetComponentsInChildren<RowInterfaceP2>();
        interval1 = timeInterval;
        shot1 = 0;
        shot2 = 0;
        active = false;
        foreach (RowInterfaceP1 r in _rows1){
            r.Shot(shot1);
        }
        foreach (RowInterfaceP2 r in _rows2)
        {
            r.Shot(shot1);
        }
    }

    void Update()
    {

        if (interval1 >= 0) {

            interval1 -= velocity1 * TimeController.GetDelTaTime();
        }

        if (interval1 <= 0) {

            if (shot1 == 5)
            {
                shot1 = 0;
            }
            else {
                shot1++;
            }

            foreach (RowInterfaceP1 r in _rows1){
                r.Shot(shot1);
            }
            interval1 = timeInterval;
        }


        if (!active & interval2 >= 0)
        {
            interval2 -= velocity2 * TimeController.GetDelTaTime();
        }

        if (!active & interval2 <= 0)
        {
            if (shot2 == 8)
            {
                shot2 = 0;
            }
            else
            {
                shot2++;
            }

            foreach (RowInterfaceP2 r in _rows2)
            {
                r.Shot(shot2);
            }

            interval2 = timeInterval;
        }


    }

    public void ActivePart2() {
        active = true;
        foreach (RowInterfaceP2 r in _rows2)
        {
            r.Active();
        }
    }

    public void DeActivePart2()
    {
        if (active)
        {
            active = false;
            interval2 = timeInterval;
            shot2 = 0;
            foreach (RowInterfaceP2 r in _rows2)
            {
                r.Shot(shot2);
            }
        }
    }

    public GameObject GetCheckpoint() {
        return checkPoint;
    }
}
