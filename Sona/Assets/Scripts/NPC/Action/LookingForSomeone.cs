using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookingForSomeone : Action {

    /**
     * 
     */
    public override void Act(GuardController controller)
    {
        controller.Idle();
        controller.agent.isStopped = true;
        controller.lockSpright.enabled = false;
        Decision decision = new ScanDecision();
        /*
        float distance = Vector3.Distance(controller.transform.position, controller.getInitialPosition());
        
        if (distance > 2f || distance < 0)
        {
            controller.agent.SetDestination(controller.getInitialPosition());
        }
        */

        controller.setAction(decision.Decide(controller));
    }

}
