using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LookDecision : Decision
{

    private IEnumerator coroutine;
    public float WaitTime;
    private float completionTime;

    /**
     * 
     */
    public override Action Decide(GuardController controller)
    {
        Action nextAction;
        if ((controller.Investigate() || controller.DetectPlayer() < controller.catchingRadius) && controller.HannaIsVisible())
        {
            //Debug.Log("I'm chasing!");
            nextAction = new Chase();
        }
        else
        {
            //Debug.Log("Ramo else look Decision");
            nextAction = new LookingForSomeone();
            //controller.changedStateLately = false;
        }
        return nextAction;
    }


}
