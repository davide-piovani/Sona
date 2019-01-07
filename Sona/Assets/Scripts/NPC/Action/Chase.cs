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
        Decision decision = new LookDecision();
        controller.setAction(decision.Decide(controller));
        ChaseTarget(controller);
    }

    /**
     * 
     */
    private void ChaseTarget(GuardController controller)
    {
        //EventManager.GuardSpottedPlayer();
        //controller.lastTarget = controller.target;
        if (!controller.actionCompleted)
        {
            Debug.Log("Chasing last pos: " + controller.lastTarget.position);
            Debug.Log("INITIAL POS: " + controller.initialPosition.position);
            controller.MoveTo(controller.lastTarget);
        }
        else
        {
            Debug.Log("Chasing player");
            controller.MoveTo(controller.target);
        }

        controller.GuardCatchPlayer();
        //Debug.Log("I'm moving to: " + controller.target.name);
    }


}
