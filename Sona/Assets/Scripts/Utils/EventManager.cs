using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour {

    public delegate void AllarmIsActivated();
    public static event AllarmIsActivated AllarmIsActivatedMethods;

    public delegate void GuardIsCalling();
    public static event GuardIsCalling GuardIsCallingMethods;

    public delegate void ChangeState();
    //public static event ChangeState ChangeStateMethods;


    public static void AllarmRinging()
    {
        //Debug.Log("The allarm is ringing!");
        AllarmIsActivatedMethods();
    }

    public static void GuardSpottedPlayer()
    {
        //Debug.Log("A guard spotted you! Go away, you fool!");
        GuardIsCallingMethods();
    }

    public static void ChangeStateGuards()
    {
        //Debug.Log("Guards are changing state");
        AllarmIsActivatedMethods();
    }
}
