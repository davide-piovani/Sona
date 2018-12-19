using System;
using UnityEngine;

namespace ApplicationConstants
{

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

        public static float runningSpeed = 4f;
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

        public static string killingObjectTag = "KillingObject";

        public static string playerTag = "Player";
    }

    public class GuardConstants {
        public static float guardWalkingSpeed = 1f;
        public static float guardRunningSpeed = 2.2f;
        public static float guardVisionAngle = 85f;
        public static float playerCatchedMaxDistance = 1.4f;

        public static int[] guardSightCollisionLayers = {9, 11};

        public static string guardTag = "Guard";
    }

    public class MenuConstants {
        public static Color selectedButtonColor = Color.white;
        public static Color unselectedButtonColor = Color.blue;

        public static bool[][] menuActiveButtons = {
            new bool[] {true, true, true, true, true},
            new bool[] {true, true, true, false, true}
        };

        public static string[][] menuButtonsText = {
            new string[] {"New Game", "Load Game", "Select Level", "Settings", "Prototypes"},
            new string[] {"Jack Level", "Charlie Level", "Hannah Level", "", "Back"}
        };
    }

    public class SceneNames {

	public static string menu = "0. Main menù";
        public static string prototype1 = "Jack Level";
        public static string prototype2 = "Charlie level";
        public static string prototype3 = "Hannah Level";
    }
}