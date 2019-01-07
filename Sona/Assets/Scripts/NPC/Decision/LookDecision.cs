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
            nextAction = new Chase();
            controller.actionCompleted = true;
        }
        else
        {
            //if (!controller.actionCompleted) nextAction = new Chase();
            //else 
            controller.SetLastTarget();

            float distance = Vector3.Distance(controller.transform.position, controller.lastTarget.position);
            Debug.Log("Distance: " + distance);

            if (distance > 1f)
            {
                controller.actionCompleted = false;
                nextAction = new Chase();
                //controller.agent.SetDestination(controller.lastTarget.position);
            }
            else
            {
                Debug.Log("Looking for someone");
                controller.actionCompleted = true;
                nextAction = new LookingForSomeone();
            }


        }
        return nextAction;
    }


}
