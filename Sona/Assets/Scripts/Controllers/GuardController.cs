using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuardController : MonoBehaviour {

    GuardState state;

    float lookRadius, distance;

    GameObject allarm;
    GuardGruoup guardsGroup;

    //Variables for line of sight
    public float heightMultiplier;
    public float viewDistance = 10f;

    //variables for investigation
    private Vector3 investigateSpot;
    //private float timer = 0;
    //TODO: use timer to make guards patrol if they don't find the player
    public float investigateWait = 10;

    Transform target;
    NavMeshAgent agent;

    // Use this for initialization
    void Start()
    {
        GuardState currentState = new AllertState();
        ChangeState(currentState);
        //target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();

        heightMultiplier = 1.36f;
    }

    // Update is called once per frame
    void Update()
    {
        Investigate();   
    }

    /**
     * This method is used by guards to find the player
     */
    void Investigate()
    {
        //timer = Time.deltaTime;
        RaycastHit hit;
        //visual rappresentation of this ray
        Debug.DrawRay(transform.position + Vector3.up * heightMultiplier, transform.forward * viewDistance, Color.green);
        Debug.DrawRay(transform.position + Vector3.up * heightMultiplier, (transform.forward + transform.right).normalized * viewDistance, Color.green);
        Debug.DrawRay(transform.position + Vector3.up * heightMultiplier, (transform.forward - transform.right).normalized * viewDistance, Color.green);
        //Look for player in the three directions 
        if (Physics.Raycast (transform.position + Vector3.up * heightMultiplier, transform.forward, out hit, viewDistance))
        {
            if (hit.collider.gameObject.tag == "Player")
            {
                target = hit.collider.gameObject.transform;
            }
        }
        if (Physics.Raycast(transform.position + Vector3.up * heightMultiplier, (transform.forward + transform.right).normalized, out hit, viewDistance))
        {
            if (hit.collider.gameObject.tag == "Player")
            {
                target = hit.collider.gameObject.transform;
            }
        }
        if (Physics.Raycast(transform.position + Vector3.up * heightMultiplier, (transform.forward - transform.right).normalized, out hit, viewDistance))
        {
            if (hit.collider.gameObject.tag == "Player")
            {
                target = hit.collider.gameObject.transform;
            }
        }
        if (target != null)
        {
            agent.SetDestination(target.position);
            EventManager.GuardSpottedPlayer();
            transform.LookAt(investigateSpot);
            distance = DetectPlayer();
            if (distance < 0)
            {
                target = StopFollow(target);
            }
        }


    }

    /**
     * This method is used to change guard state
     */
    public void ChangeState(GuardState newState)
    {
        state = newState;
        lookRadius = state.GetRadius();
        Debug.Log(lookRadius);
    }

    /**
    * This method is called if player hides
    */
    Transform StopFollow(Transform target)
    {
        agent.SetDestination(target.position);
        return null;
    }

    /**
     * This method is called in order to go in a specific point
     */
    internal void MoveTo(Transform transform)
    {
        agent.SetDestination(transform.position);
    }

    /**
     * This method is used 
     * @return  distance positive if a guard detect the player
     *          distance negative if player is not detected
     */
    float DetectPlayer()
    {
        float distance = Vector3.Distance(target.position, transform.position);

        if (distance <= lookRadius)
        {
            return distance;
        }
        return -1;
    }

    /**
     * This method is used when a guard reachs the player. Player was catch and the game ends
     */
    void EndGame()
    {
        Debug.Log("Guard reach the player, you lose!");
    }

    /**
     * This method is used in order to make guard to face player
    */
    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        //new Vector just to avoid any changing in direction
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z)); 
        //Quaternion slerp to get not istant rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    //show lookradius 
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

}
