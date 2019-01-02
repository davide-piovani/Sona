using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchDecision : Decision
{
    public override Action Decide(GuardController controller)
    {
        float distanceFromLastPosition = Vector3.Distance(controller.allarmTransform.position, controller.transform.position);
        //Debug.Log("Seatch decision tree");
        if ( (distanceFromLastPosition < 5f) && !controller.Investigate())
        {
            //Debug.Log("Looking for someone");
            controller.MoveTo(controller.allarmTransform);
            return new LookingForSomeone();
        }
        else
        {
            if (controller.Investigate())
            {
                //Debug.Log("Chase tree decision");
                return new Chase();
            }
            else
            {
                //Debug.Log("MovingSomewhere");
                //Debug.Log("Distance: " + distanceFromLastPosition);
                //Debug.Log("Investigate: " + controller.Investigate());
                return new MovingSomewhere();
            }
        }
    }
}
