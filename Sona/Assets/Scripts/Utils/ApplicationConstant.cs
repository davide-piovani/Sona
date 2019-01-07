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
        public static string createNewSlotAlertMessage = "Create a new game on this slot?";
        public static string loadSlotAlertMessage = "Are you sure you want to load this slot?";

        public static float defaultMusicVolume = 1f;
        public static float defaultEffectsVolume = 1f;
    }

    public class CameraConstants{
        //Camera Controller
        public static string CameraUp = "CamUp";
        public static string CameraDown = "CamDown";
        public static string CameraLeft = "CamLeft";
        public static string CameraRight = "CamRight";
    }

    public class PlayersConstants{
        public static int playerLayer = 9;
        public static int dashableObjectsLayer = 10;

        public static float runningMinimumMagnitude = 0.5f;
        public static float runningSpeed = 4f;
        public static float walkingSpeed = 1f;
        public static float animationsSpeed = 0.5f;

        public static float jackPowerDuration = 10f;
        public static float charliePowerDuration = 5f;
        public static float hannahPowerDuration = 5f;
        public static float jackRechargeSpeed = 2f;
        public static float charlieRechargeSpeed = 2f;
        public static float hannahRechargeSpeed = 2f;

        public static string powerButtonName = "PowerButton";
        public static string changeCharacterButton = "ChangeCharacterButton";
        public static string enterButton = "EnterButton";
        public static string jumpButton = "Jump";
        public static string pauseButton = "PauseButton";
        public static string interactButton = "InteractButton";

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
            new bool[] {true, true, true, false, false},     //Main menu
            new bool[] {true, true, true, false, true},     //New Game
            new bool[] {true, true, true, false, true},     //Load Game
            new bool[] {true, true, true, false, true}      //Select Level
        };

        public static string[][] menuButtonsText = {
            new string[] {"New Game", "Load Game", "Select Level", "", ""},     //Main menu
            new string[] {GameConstants.voidSlotName, GameConstants.voidSlotName, GameConstants.voidSlotName, GameConstants.voidSlotName, "Back"},       //New Game
            new string[] {GameConstants.voidSlotName, GameConstants.voidSlotName, GameConstants.voidSlotName, GameConstants.voidSlotName, "Back"},       //Load Game
            new string[] { "First Level", "Intermediate Level", "Final Level", "", "Back"},     //Select level
        };
    }

    public class SceneNames {

	    public static string menu = "0. Main menù";

        public static string level1 = "1. First Level";
        public static string level2 = "2. Intermediate Level";
        public static string level3 = "3. FinalLevel";

        public static string prototype1 = "Jack Level";
        public static string prototype2 = "Charlie level";
        public static string prototype3 = "Hannah Level";
    }
}