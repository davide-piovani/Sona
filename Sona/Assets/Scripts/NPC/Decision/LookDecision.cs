using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LookDecision : Decision
{

    private IEnumerator coroutine;
    public float WaitTime;
    private float completionTime;

    /**
     * 
     */
    public override Action Decide(GuardController controller)
    {
        Action nextAction;
        bool targetVisible = Investigate(controller);
        Debug.Log("LookDecision: " + targetVisible);
        if (targetVisible)
        {
            Debug.Log("I'm chasing!");
            nextAction = new Chase();
        }
        else
        {
            //EventManager.StopFollow();
            controller.changedStateLately = false;
            //Debug.Log("LookDecision else branch");
            nextAction = new LookingForSomeone();
        }
        return nextAction;
    }

    /*
    IEnumerator Scanning(GuardController controller)
    {
        //TODO da perferzionare
        bool targetVisible = Look(controller);
        if (targetVisible)
        {
            yield break;
        }
        else
        {
            yield return new WaitForSeconds(1);
        }

    }
    */

    /**
 * This method is used by guards to find the player
 */
    bool Investigate(GuardController controller)
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
                return true;
                target = hit.collider.gameObject.transform;
            }
        }
        if (Physics.Raycast(controller.transform.position + Vector3.up * controller.heightMultiplier, (controller.transform.forward + controller.transform.right).normalized, out hit, controller.viewDistance))
        {
            if (hit.collider.gameObject.tag == "Player")
            {
                return true;
                target = hit.collider.gameObject.transform;
            }
        }
        if (Physics.Raycast(controller.transform.position + Vector3.up * controller.heightMultiplier, (controller.transform.forward - controller.transform.right).normalized, out hit, controller.viewDistance))
        {
            if (hit.collider.gameObject.tag == "Player")
            {
                return true;
                //target = hit.collider.gameObject.transform;
            }
        }
        return false;
        return target;
    }

}
