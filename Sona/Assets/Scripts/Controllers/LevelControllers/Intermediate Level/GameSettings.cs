using ApplicationConstants;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : MonoBehaviour {

    static string playMode = "HISTORY";      /*   value allowed: LEVEL, HYSTORY   */
    static string controller = "KEYBOARD";      /*   value allowed: KEYBOARD, XBOX, PLAYSTATION   */

    public static void SetControllerType(string str) { controller = str; }
    public static void SetPlayMode(string str) { playMode = str; }

    public static string GetPlayMode() { return playMode; }
    public static string GetControllerType() { return controller; }

}
