using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolDecision : Decision {

    /**
     * 
     */
    public override Action Decide(GuardController controller)
    {
        float playerDetected = controller.DetectPlayer();
        if (playerDetected < 0)
        {
            return new Patrolling();
        }
        else
        {
            if (controller.Investigate())
            {
                return new Chase();
            }
            else
            {
                return new LookingForSomeone();
            }
        }
    }
   
}
