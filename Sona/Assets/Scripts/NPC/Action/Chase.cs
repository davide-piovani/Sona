using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : Action
{
    /**
     * 
     */
    public override void Act(GuardController controller)
    {
        controller.agent.isStopped = false;

        Decision decision = new LookDecision();

        if (decision.Decide(controller).GetType() != typeof(Chase))
        {
            controller.setAction(new LookingForSomeone());
        }
        /*
        if (controller.GameManager.playerIsSpottable)
        {
            Debug.Log("Il giocatore è: " + controller.GameManager.playerIsSpottable);
            ChaseTarget(controller);
        }
        */

    }

    public override bool ActionComplete(GuardController controller)
    {
        throw new System.NotImplementedException();
    }

    /**
     * 
     */
    private void ChaseTarget(GuardController controller)
    {
        EventManager.GuardSpottedPlayer();
        controller.MoveTo(controller.target);
        Debug.Log("I'm moving to: " + controller.target.name);
    }



}
