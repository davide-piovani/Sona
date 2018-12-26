using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookingForSomeone : Action {

    float currentTime;

    /**
     * 
     */
    public override void Act(GuardController controller)
    {
        Debug.Log("LookingForSomeone action");
        controller.agent.isStopped = true;
        bool targetVisible = Investigate(controller);

        Decision decision = new ScanDecision();
        /*
        if (decision.Decide(controller).GetType() == typeof(Chase) && controller.GameManager.playerIsSpottable)
        {
            controller.setAction(new Chase());
        }
        else
        {
            controller.setAction(decision.Decide(controller));
        }
        */
    }

    /**
     * 
    */
    private bool Look(GuardController controller)
    {
        RaycastHit hit;

        Debug.DrawRay(controller.transform.position, controller.transform.forward.normalized * controller.viewDistance, Color.green);
        /*
        if (Physics.SphereCast(controller.transform.position, controller.currentState.catchingRadius, controller.transform.forward, out hit, controller.viewDistance) && hit.collider.CompareTag("Player"))
        {
            controller.target = hit.transform;
            return true;
        }
        else
        {
            return false;
        }
        */
        return false;
    }
 
       
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
        if (Physics.Raycast (controller.transform.position + Vector3.up * controller.heightMultiplier, controller.transform.forward, out hit, controller.viewDistance))
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

    public override bool ActionComplete(GuardController controller)
    {
        throw new System.NotImplementedException();
    }
}
