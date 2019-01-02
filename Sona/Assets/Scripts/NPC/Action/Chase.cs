using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Chase : Action
{
    Transform target;
    /**
     * 
     */
    public override void Act(GuardController controller)
    {
        controller.agent.isStopped = false;
        controller.lockSpright.enabled = true;
        controller.Run();
        ChaseTarget(controller);
        Decision decision = new LookDecision();
        controller.setAction(decision.Decide(controller));
    }

    /**
     * 
     */
    private void ChaseTarget(GuardController controller)
    {
        EventManager.GuardSpottedPlayer();
        controller.lastTarget = controller.target;
        controller.MoveTo(controller.target);
        controller.GuardCatchPlayer();
        //Debug.Log("I'm moving to: " + controller.target.name);
    }


}
