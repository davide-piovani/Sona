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
        //float currentTime = 0;
        Action nextAction;
        Transform noPlayerInsight = Investigate(controller);
        if (noPlayerInsight != null)
            //&& controller.GameManager.playerIsSpottable)
        {
            nextAction = new Chase();
        }
        else
        {
            if (DetectPlayer(controller) < 0)
            {
                nextAction = new Patrolling();
            }
            else
            {
                nextAction = new LookingForSomeone();
            }
        }
        return nextAction;
    }


    /**
    * This method is used by guards to find the player
    */
    Transform Investigate(GuardController controller)
    {
        Transform target = null;
        //timer = Time.deltaTime;
        RaycastHit hit;
        //visual rappresentation of this ray
        Debug.DrawRay(controller.transform.position + Vector3.up * controller.heightMultiplier, controller.transform.forward * controller.viewDistance, Color.green);
        Debug.DrawRay(controller.transform.position + Vector3.up * controller.heightMultiplier, (controller.transform.forward + controller.transform.right).normalized * controller.viewDistance, Color.green);
        Debug.DrawRay(controller.transform.position + Vector3.up * controller.heightMultiplier, (controller.transform.forward - controller.transform.right).normalized * controller.viewDistance, Color.green);
        //Look for player in the three directions 
        if (Physics.Raycast(controller.transform.position + Vector3.up * controller.heightMultiplier, controller.transform.forward, out hit, controller.viewDistance))
        {
            if (hit.collider.gameObject.tag == "Player")
            {
                target = hit.collider.gameObject.transform;
            }
        }
        if (Physics.Raycast(controller.transform.position + Vector3.up * controller.heightMultiplier, (controller.transform.forward + controller.transform.right).normalized, out hit, controller.viewDistance))
        {
            if (hit.collider.gameObject.tag == "Player")
            {
                target = hit.collider.gameObject.transform;
            }
        }
        if (Physics.Raycast(controller.transform.position + Vector3.up * controller.heightMultiplier, (controller.transform.forward - controller.transform.right).normalized, out hit, controller.viewDistance))
        {
            if (hit.collider.gameObject.tag == "Player")
            {
                target = hit.collider.gameObject.transform;
            }
        }
        float distance = DetectPlayer(controller);
        Debug.Log("Distance from player: " + distance);
        if (distance < 0)
        {
            return null;
        }
        return target;
    }

    /**
 * This method is used 
 * @return  distance positive if a guard detect the player
 *          distance negative if player is not detected
 */
    float DetectPlayer(GuardController controller)
    {
        float distance = Vector3.Distance(controller.target.position, controller.transform.position);

        if (distance <= controller.lookRadius)
        {
            return distance;
        }
        Debug.Log("Distance from player: " + distance);
        return -1;
    }

}
