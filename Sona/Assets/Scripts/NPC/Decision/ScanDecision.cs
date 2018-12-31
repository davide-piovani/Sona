using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScanDecision : Decision
{

    /**
     * 
     */
    public override Action Decide(GuardController controller)
    {
        bool waypointsSetted = true;
            if (controller.DetectPlayer() < 0)
            {
                if (controller.waypoints.Length > 0)
                {
                    for (int i = 0; i < controller.waypoints.Length; i++)
                    {
                        if (controller.waypoints[i] == null)
                        {
                            waypointsSetted = false;
                        }
                    }
                    if (waypointsSetted)
                    {
                    return new Patrolling();
                    }
                return new LookingForSomeone();
                }
                else
                {
                return new LookingForSomeone();
            }
            }
            else
            {
                controller.FaceTarget(controller.GetPlayerPosition());
                if (controller.Investigate() || controller.DetectPlayer() < controller.catchingRadius)
                {
                    return new Chase();
                }
            return new LookingForSomeone();
            }
    }



}
