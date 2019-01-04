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
        controller.setAction(decision.Decide(controller));
    }

}
