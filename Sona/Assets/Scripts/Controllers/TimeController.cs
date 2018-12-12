using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour {

    static float slowDeltaTime = 0.1f;
    static bool slow = false;

    public static float GetDelTaTime() {

        if (slow)
        {
            return slowDeltaTime * Time.deltaTime;
        }
        else {
            return Time.deltaTime;
        }
    }

    public static void changeTime(bool slowTime) {
        slow = slowTime;
    }

}
