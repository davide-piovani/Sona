using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookingForSomeone : Action {

    /**
     * 
     */
    public override void Act(GuardController controller)
    {
        controller.agent.isStopped = false;
        controller.lockSpright.enabled = false;
        Decision decision = new ScanDecision();

        if (!controller.followingAlarm){
            float distance = Vector3.Distance(controller.transform.position, controller.getInitialPosition());

            if (distance > 1f)
            {
                controller.Walk();
                controller.agent.SetDestination(controller.getInitialPosition());
            }
            else
            {
                controller.Idle();
                controller.RestoreInitialRotation();
            }
        }


        controller.setAction(decision.Decide(controller));
    }

}
