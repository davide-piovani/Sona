using System;
using UnityEngine;

namespace ApplicationConstants {


    public class GameConstants {
        public static string gameExtension = ".sona";
        public static string storagePath = Application.persistentDataPath;
        public static string gameSlotPath = storagePath + "/gameSlot";

        public static float alertWidth = 1120f;
        public static float alertHeight = 460f;
        public static float alarmSwitchTime = 0.4f;

        public static string cancelString = "Cancel";
        public static string okString = "Proceed";

        public static string voidSlotName = "---";
        public static string createNewSlotAlertMessage = "Create a new game on this slot";
        public static string overwriteSlotAlertMessage = "If you continue data will be overwritten";
        public static string loadSlotAlertMessage = "Load this slot";

        public static float defaultMusicVolume = 1f;
        public static float defaultEffectsVolume = 1f;

        public static string keyboardPad = "Keyboard";
        public static string xboxPad = "Xbox Pad";
        public static string playStationPad = "PlaystationPad";

        public static string levelMode = "level";
        public static string historyMode = "history";
    }


    public class ButtonConstants {
        public static string powerButtonName_KEYBOARD = "powerButton_Keyboard";
        public static string changePlayerButtonName_KEYBOARD = "changePlayerButton_Keyboard";
        public static string interactButtonName_KEYBOARD = "interactButton_Keyboard";
        public static string pauseButtonName_KEYBOARD = "pauseButton_Keyboard";
        public static string enterButtonName_KEYBOARD = "enterButton_Keyboard";

        public static string powerButtonName_XBOX = "powerButton_Xbox";
        public static string changePlayerButtonName_XBOX = "changePlayerButton_Xbox";
        public static string interactButtonName_XBOX = "interactButton_Xbox";
        public static string pauseButtonName_XBOX = "pauseButton_Xbox";
        public static string enterButtonName_XBOX = "enterButton_Xbox";

        public static string powerButtonName_PLAY = "powerButton_Play";
        public static string changePlayerButtonName_PLAY = "changePlayerButton_Play";
        public static string interactButtonName_PLAY = "interactButton_Play";
        public static string pauseButtonName_PLAY = "pauseButton_Play";
        public static string enterButtonName_PLAY = "enterButton_Play";
    }


    public class AxisConstants {
        public static string axisXName_KEYBOARD = "axisX_Keyboard";
        public static string axisYName_KEYBOARD = "axisY_Keyboard";

        public static string axisXName_XBOX = "axisX_Xbox";
        public static string axisYName_XBOX = "axisY_Xbox";

        public static string axisXName_PLAY = "axisX_Play";
        public static string axisYName_PLAY = "axisY_Play";
    }


    public class CameraConstants{
        public static string cameraXaxisName_KEYBOARD = "cameraXaxis_Keyboard";
        public static string cameraYaxisName_KEYBOARD = "cameraYaxis_Keyboard";

        public static string cameraXaxisName_XBOX = "cameraXaxis_Xbox";
        public static string cameraYaxisName_XBOX = "cameraYaxis_Xbox";

        public static string cameraXaxisName_PLAY = "cameraXaxis_Play";
        public static string cameraYaxisName_PLAY = "cameraYaxis_Play";
    }


    public class PlayersConstants{
        public static int playerLayer = 9;
        public static int dashableObjectsLayer = 10;

        public static float runningMinimumMagnitude = 0.5f;
        public static float runningSpeed = 4f;
        public static float walkingSpeed = 1f;
        public static float animationsSpeed = 0.5f;

        public static float jackPowerDuration = 35f;
        public static float charliePowerDuration = 15f;
        public static float hannahPowerDuration = 6f;
        public static float jackRechargeSpeed = 15f;
        public static float charlieRechargeSpeed = 10f;
        public static float hannahRechargeSpeed = 2f;

        public static string powerButtonName = "PowerButton";
        public static string changeCharacterButton = "ChangeCharacterButton";
        public static string enterButton = "EnterButton";
        public static string jumpButton = "Jump";
        public static string pauseButton = "PauseButton";
        public static string interactButton = "InteractButton";
        public static string x_Axis = "X_Axis";
        public static string y_Axis = "Y_Axis";

        public static string killingObjectTag = "KillingObject";
        public static string playerTag = "Player";
    }


    public class GuardConstants {
        public static float guardWalkingSpeed = 10f;
        public static float guardRunningSpeed = 40f;

        public static float guardLookRadius = 15f;
        public static float guardCatchingRadius = 4f;

        public static float guardVisionAngle = 85f;
        public static float playerCatchedMaxDistance = 1.4f;

        public static int[] guardSightCollisionLayers = {9, 10, 11};

        public static string guardTag = "Guard";
    }


    public class MenuConstants {
        public static Color selectedButtonColor = new Color(61f / 255f, 108f / 255f, 1f);
        public static Color unselectedButtonColor = new Color(1f, 1f, 1f, 1f);

        public static Color sliderNormalColor = new Color(170f / 255f, 170f / 255f, 170f / 255f, 1f);
        public static Color sliderHighlightedColor = new Color(61f / 255f, 108f / 255f, 1f);
        public static float deltaSliderValue = 0.01f;

        public static bool[][] menuActiveButtons = {
            new bool[] {true, true, true, true, false},     //Main menu
            new bool[] {true, true, true, false, true},     //New Game
            new bool[] {true, true, true, false, true},     //Load Game
            new bool[] {true, true, true, false, true}      //Select Level
        };

        public static string[][] menuButtonsText = {
            new string[] {"New Game", "Load Game", "Select Level", "Settings", ""},     //Main menu
            new string[] {GameConstants.voidSlotName, GameConstants.voidSlotName, GameConstants.voidSlotName, GameConstants.voidSlotName, "Back"},       //New Game
            new string[] {GameConstants.voidSlotName, GameConstants.voidSlotName, GameConstants.voidSlotName, GameConstants.voidSlotName, "Back"},       //Load Game
            new string[] { "Level1", "Level2", "Level3", "", "Back"},     //Select level
        };
    }


    public class SceneNames {
	    public static string menu = "MainMenu";

        public static string level1 = "Level1";
        public static string level2 = "Level2.1";
        public static string level3 = "Level3";

        public static string transition = "Transition";

        public static string prototype1 = "Jack Level";
        public static string prototype2 = "Charlie level";
        public static string prototype3 = "Hannah Level";
    }
}