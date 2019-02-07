using ApplicationConstants;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : MonoBehaviour {

    static int currentSceneNumber = 1; //Main Menu
    static string playMode = GameConstants.historyMode; //default Value
    static string controller = GameConstants.keyboardPad; //default Value

    public static void SetCurrentSceneNumber(int num) { currentSceneNumber = num; }
    public static void SetControllerType(string str) { controller = str; }
    public static void SetPlayMode(string str) { playMode = str; }

    public static int GetCurrentSceneNumber() { return currentSceneNumber; }
    public static string GetPlayMode() { return playMode; }
    public static string GetControllerType() { return controller; }

}
