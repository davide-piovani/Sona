using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Patrolling : Action
{ 
    Transform patrolDestination;

    public override void Act(GuardController controller)
    {

        controller.lockSpright.enabled = false;
        Patrol(controller);
    }

    private void Patrol(GuardController controller)
    {
        controller.Walk();
        bool wayPointsReady = waypointsReady(controller);
        if (wayPointsReady)
        {
            patrolDestination = FindDestination(controller);
            controller.MoveTo(patrolDestination);
        }
        controller.agent.isStopped = false;
        
        Decision decision = new PatrolDecision();

        if (decision.Decide(controller).GetType() != typeof(Patrolling))
        {
            controller.setAction(new LookingForSomeone());
        }
        
    }

    bool waypointsReady(GuardController controller)
    {
        for (int i=0; i< controller.waypoints.Length; i++)
        {
            if (controller.waypoints[i] == null)
                return false;
        }
        return true;
    }

    /**
     * Find the min distance among waypoints
     */
    Transform FindDestination(GuardController controller)
    {
        Transform destination;
        //Debug.Log("variable: " + controller.changedStateLately);
        //if (controller.noWaypointSetted)
        /*{
            destination = controller.waypoints[0];
            float distance = Vector3.Distance(controller.waypoints[0].position, controller.transform.position);
            Debug.Log("Distance: " + distance);
            if (distance < 1.5f)
            {
                controller.Idle();
            }
            return destination;
        }*/
       // else
       // {
            //get closer waypoint only when i change state to investigate
            if (!controller.changedStateLately)
            {
                destination = CloserWaypoint(controller);
                controller.changedStateLately = true;
                //Debug.Log("variable: " + controller.changedStateLately);
                return destination;
            }
            else
            {
                Transform finalDestination = null;
                //I scan waypoints array in order to find index of my destination
                for (int i = 0; i < controller.waypoints.Length; i++)
                {
                    //Debug.Log("Array Lenght: " + controller.waypoints.Length);
                    destination = controller.waypoints[i];
                    //if I dind that destination fit waypoint array element at i index then I save the index
                    if (destination.Equals(patrolDestination))
                    {
                        finalDestination = calcolateNextWaypoint(destination, i, controller);
                    }
                }
                if (finalDestination == null)
                {
                    //Debug.Log("finalDestination è null OMG! Had to reset it!");
                    finalDestination = CloserWaypoint(controller);
                }
                //Debug.Log("Moving to: " + finalDestination.name);
                return finalDestination;

            }
        //}  
    }

    private Transform calcolateNextWaypoint(Transform destination, int i, GuardController controller)
    {
        Transform finalDestination;
        if (distanceToWaypoint(destination, controller) > 1f)
        //if (DestinationReached(controller.agent))
            finalDestination = destination;
        else
        {
            finalDestination = StopAndWait(i, controller);
        }
        return finalDestination;
    }


    private Transform StopAndWait(int i, GuardController controller)
    {
        Transform finalDestination;
        finalDestination = ArrayIndexOrder(i, controller);
        controller.FaceTarget(finalDestination);
        return finalDestination;
    }


    private bool CalculateWaypoint(int i, GuardController controller)
    {
        if (i + 1 == controller.waypoints.Length)
            return true;
        if (i - 1 < 0)
            return false;
        return controller.descentOrder;
    }

    private Transform ArrayIndexOrder(int i, GuardController controller)
    {
        Transform destinationWaypoint;
        controller.descentOrder = CalculateWaypoint(i, controller);
        if (!controller.descentOrder)
        {
            destinationWaypoint = controller.waypoints[i + 1].transform;
        }
        else
        {
            destinationWaypoint = controller.waypoints[i - 1].transform;
        }
        return destinationWaypoint;
    }

    /**
 * This method calculates the distance between a gameObject and the waypoint
 */
    private float distanceToWaypoint(Transform gameObjectTransform, GuardController controller)
    {
        float dist = Vector3.Distance(gameObjectTransform.position, controller.transform.position);
        return dist;
    }

    /**
     * This method is used to get the closer waypoint
     */
    Transform CloserWaypoint(GuardController controller)
    {
        Transform destination = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPos =controller.transform.position;
        foreach (Transform waypointsDistance in controller.waypoints)
        {
            Vector3 directionToTarget = waypointsDistance.position - currentPos;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                destination = waypointsDistance;
            }
        }
        /*
        if (destination == null)
        {
            Debug.Log("Destination è null OMG!");
        }
        */
        return destination;
    }
}