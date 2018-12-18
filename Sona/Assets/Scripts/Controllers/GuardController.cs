using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuardController : MonoBehaviour {

    //public GuardState currentState;
    //GuardState remainState;

    public float lookRadius;
    float distance;

    Decision decision;

    Animator anim;
    GameObject allarm;
    GuardGruoup guardsGroup;

    public Action currentAction;
    Action lastAction;

    public PlayerManager GameManager;

    //Variables for line of sight
    public float heightMultiplier;
    public float viewDistance = 10f;
    public float searchingTurnSpeed = 0.2f;
    public float catchingRadius = 10f;

    //public int waypointNumber;

    //private bool waiteInvestigationTime;

    //path variables
    public Transform[] waypoints;
    public GameObject firstWaypoint;
    public GameObject secondWaypoint;
    Transform patrolDestination;

    public bool changedStateLately;

    //variables for investigation
    private Vector3 investigateSpot;
    //private float frame = 0;
    //TODO: use timer to make guards patrol if they don't find the player
    public float investigateWait = 150f;
    //private float curTime;

    //Action[] actionsPossible;

    public float stateTimeElapsed;

    public Transform target;
    public NavMeshAgent agent;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        SetupWaypoints();
        changedStateLately = false;

        //setActions();
        //frame = 0;
        //currentState = new InvestigateState();
        //Debug.Log(currentState.name);
        //SetInitialState(currentState);
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        //waiteInvestigationTime = true;
        heightMultiplier = 1.36f;
        lookRadius = 15;
        GuardGruoup guardGroup = GetComponentInParent<GuardGruoup>();
        //setInitialState(guardGroup);

    }


    // Update is called once per frame
    void Update()
    {
        if (currentAction == null)
        {
            currentAction = setInitialState(GetComponentInParent<GuardGruoup>());
        }
        //currentAction = DecisionMakingProcess();
        //NextDecision = currentAction.getDecisoner();
        /*
        if (!changedStateLately)
        {
            lastAction = currentAction;
        }
        if (lastAction.GetType() == currentAction.GetType())
        {
            changedStateLately = false;
        }
        */
        currentAction.Act(this);
    }

    Action setInitialState(GuardGruoup guardGroup)
    {
        Action initialAction = guardGroup.initialState;
        //Debug.Log("Initial action: " + initialAction.name);
        return initialAction;
    }

    public Transform GetPlayerPosition()
    {
        return PlayerManager.instance.player.transform;
    }

    /*
    void setActions()
    {
        actionsPossible[0] = new Patrolling();
        actionsPossible[1] = new LookingForSomeone();
        actionsPossible[2] = new Chase();
        actionsPossible[3] = new Relax();
        actionsPossible[4] = new Sleep();
    }
    */

    public void setAction(Action action)
    {
        currentAction = action;
    }

    /*
    public void TransitionToState(GuardState nextState)
    {
        if (nextState != remainState)
        {
            currentState = nextState;
            OnExitState();
        }
    }
    */

    private void OnExitState()
    {
        stateTimeElapsed = 0;
    }

    /**
     * This method is used to take a decision about the next action
    
    Action DecisionMakingProcess()
    {
        Action nextAction = null;
        Decision decision;

        if (currentAction.GetType() == typeof(Patrolling)) { }
        {
            decision = new PatrolDecision();
        }
        if (currentAction.GetType() == typeof(Chase)) { }
        {
            decision = new ScanDecision();
        }
        if (currentAction.GetType() == typeof(LookingForSomeone)) { }
        {
            decision = new LookDecision();
        }
        //TODO relax e sleep action
        nextAction = decision.Decide(this);
        return nextAction;

    }
    */
    /**
     * This method will setup waypoints
     */
    private void SetupWaypoints()
    {
        waypoints = new Transform[2];
        if (firstWaypoint == null)
        {
            waypoints[0] = this.transform;
        } 
        else
        {
            waypoints[0] = firstWaypoint.transform;
        }
        if (secondWaypoint == null )
        {
            waypoints[1] = this.transform;
        }
        else {
            waypoints[1] = secondWaypoint.transform;
        }

    }

    /**
     * Those methods are used to do something based on guard state
     * Patrolling method
     
    public void Patrolling()
    {
        patrolDestination = FindDestination();
        MoveTo(patrolDestination);
        changedStateLately = true;
    }
    
    public void LookingForSomeone()
    {
        if (frame <= 10)
        {
            //setAnimBools(Mode.idle);
            //agent.Stop();
            //agent.speed = 0;
            target = Investigate();
            Debug.Log("frame number: " + frame);
            frame++;
        }
        else
        {
            frame = 0;
            waiteInvestigationTime = true;
        }

        /*
        if (investigateWait > 0)
        {
            investigateWait -= Time.deltaTime;
        }
        else
        {
            investigateWait = 5f;
        }
        
    }

    public void Sleep()
    {

    }

    public void Relax()
    {

    }
*/
    /**
     * Find the min distance among waypoints
     
    Transform FindDestination()
    {
        Transform destination;
        //get closer waypoint only when i change state to investigate
        if (!changedStateLately)
        {
            destination = CloserWaypoint();
            changedStateLately = true;
            return destination;
        }
        else
        {
            Transform finalDestination = null;
            //I scan waypoints array in order to find index of my destination
            for (int i=0; i<waypoints.Length; i++ )
            {
                destination = waypoints[i];
                //if I dind that destination fit waypoint array element at i index then I save the index
                if (destination.Equals(patrolDestination))
                {
                    finalDestination = calcolateNextWaypoint(destination, i);
                }
            }
            if (finalDestination == null)
                Debug.Log("OMG!! Erroreeeeeee");
            return finalDestination;
        }
    }
    

    private Transform calcolateNextWaypoint(Transform destination, int i)
    {
        Transform finalDestination;
        if (distanceToWaypoint(destination) > 2f)
            finalDestination = destination;
        else
        {
            finalDestination = StopAndWait(i);
        }
        Debug.Log("Destinazione finale: " + finalDestination.name);
        return finalDestination;
    }
    */
    /**
    * @return   true: if destination is not the final element of waypoint array
    *           false: if destination is the final element of waypoint array
    */
    /*
    private bool CalculateWaypoint(int i)
    {
        if (i + 1 == waypoints.Length)
            return false;
        else
            return true;
    }

    private Transform ArrayIndexOrder(int i)
    {
        Transform destinationWaypoint;
        if (CalculateWaypoint(i))
            destinationWaypoint= waypoints[i + 1].transform;
        else
            destinationWaypoint = waypoints[i - 1].transform;
        return destinationWaypoint;
    }
    
    private Transform StopAndWait(int i)
    {
        Transform finalDestination;
        if (curTime == 0)
            curTime = Time.time; // Pause over the Waypoint
        while ((Time.time - curTime) < investigateWait)
        {
            finalDestination = Investigate();
        }
        curTime = 0;
        finalDestination = ArrayIndexOrder(i);
        FaceTarget(finalDestination);
        return finalDestination;
    }
    */
    /**
     * This method calculates the distance between a gameObject and the waypoint
     
    private float distanceToWaypoint (Transform gameObjectTransform)
    {

        float dist = Vector3.Distance(gameObjectTransform.position, transform.position);
        return dist;
    }
*/
    /**
     * This method is used to get the closer waypoint
     
    Transform CloserWaypoint()
    {
        Transform destination = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (Transform waypointsDistance in waypoints)
        {
            Vector3 directionToTarget = waypointsDistance.position - currentPos;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                destination = waypointsDistance;
            }
        }
        return destination;
    }
    */
    /**
     * This method is used by guards to find the player
     
    Transform Investigate()
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


            Debug.Log("Target is: " + target.name);
            agent.SetDestination(target.position);
            EventManager.GuardSpottedPlayer();
            transform.LookAt(investigateSpot);
            distance = DetectPlayer();
            waiteInvestigationTime = true;
            if (distance < 0)
            {
                target = StopFollow(target);
            }
        }
        return target;
    }
    */
    /**
     * This method is used to change guard state
     
    public void ChangeState(GuardState newState)
    {
        //state = newState;
        //lookRadius = state.GetRadius();
        Debug.Log(lookRadius);
    }
    */



    /**
    * This method is called if player hides
    */
    public void StopFollow(Transform target)
    {
        Debug.Log("Stop follow");
        agent.SetDestination(target.position);
        target = null;
    }

    /**
     * This method is called in order to go in a specific point
     */
    public void MoveTo(Transform transform)
    {
        setAnimBools(Mode.walking);
        agent = GetComponent<NavMeshAgent>();
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
        Debug.Log("Distance from player: " + distance);
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
    public void FaceTarget(Transform target)
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

    /**
     * 
    
    public bool CheckIfCountDownElapsed(float duration)
    {
        stateTimeElapsed += Time.deltaTime;
        return (stateTimeElapsed >= duration);
    }
 */ 
    //TODO: METTERE NELLE GUARDIE
    /**
     * Animation methods
     */
    enum Mode
    {
        idle,
        walking,
        running
    }

    void setAnimBools(Mode mode)
    {
        bool idle = false;
        bool walking = false;
        bool running = false;

        switch (mode)
        {
            case Mode.idle:
                idle = true;
                break;
            case Mode.walking:
                walking = true;
                break;
            case Mode.running:
                running = true;
                break;
        }

        anim.SetBool("isIdle", idle);
        anim.SetBool("isWalking", walking);
        anim.SetBool("isRunning", running);
    }

}
