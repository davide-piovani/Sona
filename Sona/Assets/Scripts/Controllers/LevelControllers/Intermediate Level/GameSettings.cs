using ApplicationConstants;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : MonoBehaviour {

    static int currentSceneNumber = 0; //Main Menu
    static string playMode = GameConstants.historyMode; //default Value
    static string controller = GameConstants.keyboardPad; //default Value

    public static void SetCurrentSceneNumber(int num) { currentSceneNumber = num; }
    public static void SetControllerType(string str) { controller = str; }
    public static void SetPlayMode(string str) { playMode = str; }

    public static int GetCurrentSceneNumber() { return currentSceneNumber; }
    public static string GetPlayMode() { return playMode; }
    public static string GetControllerType() { return controller; }


    /* MULTIPLE INPUTS MANAGER */

    /* GetButtonDown */
    public static bool GetButtonDown(string buttonName) {
        if (buttonName.Equals(PlayersConstants.interactButton))
        {
            if (controller.Equals(GameConstants.keyboardPad)) return Input.GetButtonDown(ButtonConstants.interactButtonName_KEYBOARD);
            else if (controller.Equals(GameConstants.xboxPad)) return Input.GetButtonDown(ButtonConstants.interactButtonName_XBOX);
            else if (controller.Equals(GameConstants.playStationPad)) return Input.GetButtonDown(ButtonConstants.interactButtonName_PLAY);
            else return false;
        }
        else if (buttonName.Equals(PlayersConstants.powerButtonName))
        {
            if (controller.Equals(GameConstants.keyboardPad)) return Input.GetButtonDown(ButtonConstants.powerButtonName_KEYBOARD);
            else if (controller.Equals(GameConstants.xboxPad)) return Input.GetButtonDown(ButtonConstants.powerButtonName_XBOX);
            else if (controller.Equals(GameConstants.playStationPad)) return Input.GetButtonDown(ButtonConstants.powerButtonName_PLAY);
            else return false;
        }
        else if (buttonName.Equals(PlayersConstants.changeCharacterButton))
        {
            if (controller.Equals(GameConstants.keyboardPad)) return Input.GetButtonDown(ButtonConstants.changePlayerButtonName_KEYBOARD);
            else if (controller.Equals(GameConstants.xboxPad)) return Input.GetButtonDown(ButtonConstants.changePlayerButtonName_XBOX);
            else if (controller.Equals(GameConstants.playStationPad)) return Input.GetButtonDown(ButtonConstants.changePlayerButtonName_PLAY);
            else return false;
        }
        else if (buttonName.Equals(PlayersConstants.pauseButton))
        {
            return
                Input.GetButtonDown(ButtonConstants.pauseButtonName_KEYBOARD) |
                Input.GetButtonDown(ButtonConstants.pauseButtonName_XBOX) |
                Input.GetButtonDown(ButtonConstants.pauseButtonName_PLAY);
        }
        else if (buttonName.Equals(PlayersConstants.enterButton))
        {
            return
                Input.GetButtonDown(ButtonConstants.enterButtonName_KEYBOARD) |
                Input.GetButtonDown(ButtonConstants.enterButtonName_XBOX) |
                Input.GetButtonDown(ButtonConstants.enterButtonName_PLAY);
        }
        else return false;
    }


    /* GetButton */
    public static bool GetButton(string buttonName) {
        if (buttonName.Equals(PlayersConstants.interactButton))
        {
            if (controller.Equals(GameConstants.keyboardPad)) return Input.GetButton(ButtonConstants.interactButtonName_KEYBOARD);
            else if (controller.Equals(GameConstants.xboxPad)) return Input.GetButton(ButtonConstants.interactButtonName_XBOX);
            else if (controller.Equals(GameConstants.playStationPad)) return Input.GetButton(ButtonConstants.interactButtonName_PLAY);
            else return false;
        }
        else if (buttonName.Equals(PlayersConstants.powerButtonName))
        {
            if (controller.Equals(GameConstants.keyboardPad)) return Input.GetButton(ButtonConstants.powerButtonName_KEYBOARD);
            else if (controller.Equals(GameConstants.xboxPad)) return Input.GetButton(ButtonConstants.powerButtonName_XBOX);
            else if (controller.Equals(GameConstants.playStationPad)) return Input.GetButton(ButtonConstants.powerButtonName_PLAY);
            else return false;
        }
        else if (buttonName.Equals(PlayersConstants.changeCharacterButton))
        {
            if (controller.Equals(GameConstants.keyboardPad)) return Input.GetButton(ButtonConstants.changePlayerButtonName_KEYBOARD);
            else if (controller.Equals(GameConstants.xboxPad)) return Input.GetButton(ButtonConstants.changePlayerButtonName_XBOX);
            else if (controller.Equals(GameConstants.playStationPad)) return Input.GetButton(ButtonConstants.changePlayerButtonName_PLAY);
            else return false;
        }
        else if (buttonName.Equals(PlayersConstants.pauseButton))
        {
            return
                Input.GetButton(ButtonConstants.pauseButtonName_KEYBOARD) | 
                Input.GetButton(ButtonConstants.pauseButtonName_XBOX) |
                Input.GetButton(ButtonConstants.pauseButtonName_PLAY);
        }
        else if (buttonName.Equals(PlayersConstants.enterButton))
        {
            return
                Input.GetButton(ButtonConstants.enterButtonName_KEYBOARD) |
                Input.GetButton(ButtonConstants.enterButtonName_XBOX) |
                Input.GetButton(ButtonConstants.enterButtonName_PLAY);
        }
        else return false;
    }


    /* Get Axis when Pause or Main Menu are active */
    public static float GetMenuAxis(string axis) {
        if (axis.Equals(PlayersConstants.x_Axis)) {
            float keyboard = Input.GetAxis(AxisConstants.axisXName_KEYBOARD);
            float xbox = Input.GetAxis(AxisConstants.axisXName_XBOX);
            float playstation = Input.GetAxis(AxisConstants.axisXName_PLAY);
            if (keyboard != 0) { return keyboard; }
            else if (xbox != 0) { return xbox; }
            else if (playstation != 0) { return playstation; }
            else return 0;
        }
        else if (axis.Equals(PlayersConstants.y_Axis))
        {
            float keyboard = Input.GetAxis(AxisConstants.axisYName_KEYBOARD);
            float xbox = Input.GetAxis(AxisConstants.axisYName_XBOX);
            float playstation = Input.GetAxis(AxisConstants.axisYName_PLAY);
            if (keyboard != 0) { return keyboard; }
            else if (xbox != 0) { return xbox; }
            else if (playstation != 0) { return playstation; }
            else return 0;
        }
        else return 0;
    }


    /* Get AxisRaw when Pause or Main Menu are active */
    public static float GetMenuAxisRaw(string axis)
    {
        if (axis.Equals(PlayersConstants.x_Axis))
        {
            float keyboard = Input.GetAxisRaw(AxisConstants.axisXName_KEYBOARD);
            float xbox = Input.GetAxisRaw(AxisConstants.axisXName_XBOX);
            float playstation = Input.GetAxisRaw(AxisConstants.axisXName_PLAY);
            if (keyboard != 0) { return keyboard; }
            else if (xbox != 0) { return xbox; }
            else if (playstation != 0) { return playstation; }
            else return 0;
        }
        else if (axis.Equals(PlayersConstants.y_Axis))
        {
            float keyboard = Input.GetAxisRaw(AxisConstants.axisYName_KEYBOARD);
            float xbox = Input.GetAxisRaw(AxisConstants.axisYName_XBOX);
            float playstation = Input.GetAxisRaw(AxisConstants.axisYName_PLAY);
            if (keyboard != 0) { return keyboard; }
            else if (xbox != 0) { return xbox; }
            else if (playstation != 0) { return playstation; }
            else return 0;
        }
        else return 0;
    }


    /* Get Axis when Play mode is active */
    public static float GetPlayAxis(string axis)
    {
        if (axis.Equals(PlayersConstants.x_Axis))
        {
            if (GameSettings.GetControllerType().Equals(GameConstants.keyboardPad)) return Input.GetAxis(AxisConstants.axisXName_KEYBOARD);
            else if (GameSettings.GetControllerType().Equals(GameConstants.xboxPad)) return Input.GetAxis(AxisConstants.axisXName_XBOX);
            else if (GameSettings.GetControllerType().Equals(GameConstants.playStationPad)) return Input.GetAxis(AxisConstants.axisXName_PLAY);
            else return 0;
        }
        else if (axis.Equals(PlayersConstants.y_Axis))
        {
            if (GameSettings.GetControllerType().Equals(GameConstants.keyboardPad)) return Input.GetAxis(AxisConstants.axisYName_KEYBOARD);
            else if (GameSettings.GetControllerType().Equals(GameConstants.xboxPad)) return Input.GetAxis(AxisConstants.axisYName_XBOX);
            else if (GameSettings.GetControllerType().Equals(GameConstants.playStationPad)) return Input.GetAxis(AxisConstants.axisYName_PLAY);
            else return 0;
        }
        else return 0;
    }


    /* Get AxisRaw when Play mode is active */
    public static float GetPlayAxisRaw(string axis)
    {
        if (axis.Equals(PlayersConstants.x_Axis))
        {
            if (GameSettings.GetControllerType().Equals(GameConstants.keyboardPad)) return Input.GetAxisRaw(AxisConstants.axisXName_KEYBOARD);
            else if (GameSettings.GetControllerType().Equals(GameConstants.xboxPad)) return Input.GetAxisRaw(AxisConstants.axisXName_XBOX);
            else if (GameSettings.GetControllerType().Equals(GameConstants.playStationPad)) return Input.GetAxisRaw(AxisConstants.axisXName_PLAY);
            else return 0;
        }
        else if (axis.Equals(PlayersConstants.y_Axis))
        {
            if (GameSettings.GetControllerType().Equals(GameConstants.keyboardPad)) return Input.GetAxisRaw(AxisConstants.axisYName_KEYBOARD);
            else if (GameSettings.GetControllerType().Equals(GameConstants.xboxPad)) return Input.GetAxisRaw(AxisConstants.axisYName_XBOX);
            else if (GameSettings.GetControllerType().Equals(GameConstants.playStationPad)) return Input.GetAxisRaw(AxisConstants.axisYName_PLAY);
            else return 0;
        }
        else return 0;
    }


    /* Get Camera Axis */
    public static float GetCameraAxis(string axis)
    {
        if (axis.Equals(PlayersConstants.x_Axis))
        {
            if (GameSettings.GetControllerType().Equals(GameConstants.keyboardPad)) return Input.GetAxis(CameraConstants.cameraXaxisName_KEYBOARD);
            else if (GameSettings.GetControllerType().Equals(GameConstants.xboxPad)) return Input.GetAxis(CameraConstants.cameraXaxisName_XBOX);
            else if (GameSettings.GetControllerType().Equals(GameConstants.playStationPad)) return Input.GetAxis(CameraConstants.cameraXaxisName_PLAY);
            else return 0;
        }
        else if (axis.Equals(PlayersConstants.y_Axis))
        {
            if (GameSettings.GetControllerType().Equals(GameConstants.keyboardPad)) return Input.GetAxis(CameraConstants.cameraYaxisName_KEYBOARD);
            else if (GameSettings.GetControllerType().Equals(GameConstants.xboxPad)) return Input.GetAxis(CameraConstants.cameraYaxisName_XBOX);
            else if (GameSettings.GetControllerType().Equals(GameConstants.playStationPad)) return Input.GetAxis(CameraConstants.cameraYaxisName_PLAY);
            else return 0;
        }
        else return 0;
    }


    /* Get Camera AxisRaw */
    public static float GetCameraAxisRaw(string axis)
    {
        if (axis.Equals(PlayersConstants.x_Axis))
        {
            if (GameSettings.GetControllerType().Equals(GameConstants.keyboardPad)) return Input.GetAxisRaw(CameraConstants.cameraXaxisName_KEYBOARD);
            else if (GameSettings.GetControllerType().Equals(GameConstants.xboxPad)) return Input.GetAxisRaw(CameraConstants.cameraXaxisName_XBOX);
            else if (GameSettings.GetControllerType().Equals(GameConstants.playStationPad)) return Input.GetAxisRaw(CameraConstants.cameraXaxisName_PLAY);
            else return 0;
        }
        else if (axis.Equals(PlayersConstants.y_Axis))
        {
            if (GameSettings.GetControllerType().Equals(GameConstants.keyboardPad)) return Input.GetAxisRaw(CameraConstants.cameraYaxisName_KEYBOARD);
            else if (GameSettings.GetControllerType().Equals(GameConstants.xboxPad)) return Input.GetAxisRaw(CameraConstants.cameraYaxisName_XBOX);
            else if (GameSettings.GetControllerType().Equals(GameConstants.playStationPad)) return Input.GetAxisRaw(CameraConstants.cameraYaxisName_PLAY);
            else return 0;
        }
        else return 0;
    }

}
