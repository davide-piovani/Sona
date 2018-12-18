using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolDecision : Decision {

    /**
     * 
     */
    public override Action Decide(GuardController controller)
    {
        Action nextAction;
        bool playerDetected = DetectPlayer(controller);
        if (!playerDetected)
        {
            nextAction = new Patrolling();
        }
        else
        {
            nextAction = new LookingForSomeone();
        }
        return nextAction;

    }

    /**
     * This method is used 
     * @return  distance positive if a guard detect the player
     *          distance negative if player is not detected
     */
    bool DetectPlayer(GuardController controller)
    {
        float distance = Vector3.Distance(controller.GetPlayerPosition().transform.position, controller.transform.position);

        if (distance <= controller.lookRadius)
        {
            return true;
        }
        Debug.Log("Distance from player: " + distance);
        return false;
    }
}
