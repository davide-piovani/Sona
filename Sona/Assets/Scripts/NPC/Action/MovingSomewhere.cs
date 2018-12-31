using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingSomewhere : Action
{
    public override void Act(GuardController controller)
    {
        controller.Run();
        MoveToTarget(controller);
        Decision decision = new SearchDecision();

        controller.setAction(decision.Decide(controller));
    }

    private void MoveToTarget(GuardController controller)
    {
        //Debug.Log("MovingSomewhere - Move to: " + controller.allarmTransform);
        controller.MoveTo(controller.allarmTransform);
        
    }

}
