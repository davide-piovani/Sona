using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuardController : MonoBehaviour {

    [HideInInspector] public float lookRadius;
    float distance;

    Decision decision;

    Animator anim;
    GameObject allarm;
    GuardGroup guardsGroup;

    public Action currentAction;
    //Action lastAction;

    [HideInInspector] public bool descentOrder;
    private GameController gameController;       //Da sostituire con GameController

    //Variables for line of sight
    [HideInInspector] public float heightMultiplier;
    [HideInInspector] public float viewDistance = 20f;
    private float searchingTurnSpeed = 0.2f;
    [HideInInspector] public float catchingRadius;

    [HideInInspector] public SpriteRenderer lockSpright;

    //path variables
    public Transform[] waypoints;

    //First e second da rimuovere: gestire tutto tramite waypoints
    //public GameObject firstWaypoint;
    //public GameObject secondWaypoint;
    Transform patrolDestination;

    [HideInInspector] public bool changedStateLately;

    //variables for investigation
    private Vector3 investigateSpot;
    private float investigateWait = 150f;


    private float stateTimeElapsed;
    [HideInInspector] public Transform allarmTransform;
    [HideInInspector] public Transform target;
    [HideInInspector] public Transform lastTarget;
    [HideInInspector] public NavMeshAgent agent;

    // Use this for initialization
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        changedStateLately = false;
        agent = GetComponent<NavMeshAgent>();
        lockSpright = GetComponentInChildren<SpriteRenderer>();
        lockSpright.enabled = false;
        gameController = FindObjectOfType<GameController>(); 
        agent = GetComponent<NavMeshAgent>();
        heightMultiplier = 1.36f;
        lookRadius = 13f;
        catchingRadius = 10f;
        GuardGroup guardGroup = GetComponentInParent<GuardGroup>();
        if (guardGroup.allarm != null)
        {
            allarmTransform = guardGroup.allarm.transform;
        }

    }

    // Update is called once per frame
    void Update()
    {
        this.target = gameController.GetActivePlayer().transform;
        if (currentAction == null)
        {
            currentAction = setInitialState(GetComponentInParent<GuardGroup>());
        }
        currentAction.Act(this);
    }

    Action setInitialState(GuardGroup guardGroup)
    {
        Action initialAction = guardGroup.initialState;
        return initialAction;
    }

    public bool HannaIsVisible()
    {
        if (!gameController.GetActivePlayer().IsVisible())
        {
            return false;
        }
        return true;
    }

    public Transform GetPlayerPosition()
    {
        return gameController.GetActivePlayer().transform;
    }

    public void setAction(Action action)
    {
        currentAction = action;
    }

    /**
    * This method is called if player hides
    */
    public void StopFollow(Transform target)
    {
        agent.SetDestination(target.position);
        changedStateLately = false;
    }

    /**
     * This method is called in order to go in a specific point
     */
    public void MoveTo(Transform transform)
    {
        setAnimBools(Mode.running);
        agent.SetDestination(transform.position);
    }

    /**
    * This method is used by guards to find the player
    */
    public bool Investigate()
    {
        //Transform target = null;
        //timer = Time.deltaTime;
        RaycastHit hit;
        //visual rappresentation of this ray
        Debug.DrawRay(this.transform.position + Vector3.up * heightMultiplier, this.transform.forward * viewDistance, Color.green);
        Debug.DrawRay(this.transform.position + Vector3.up * heightMultiplier, (this.transform.forward + transform.right).normalized * this.viewDistance, Color.green);
        Debug.DrawRay(this.transform.position + Vector3.up * heightMultiplier, (this.transform.forward - transform.right).normalized * this.viewDistance, Color.green);
        //Look for player in the three directions 
        if (Physics.Raycast(this.transform.position + Vector3.up * heightMultiplier, this.transform.forward, out hit, this.viewDistance))
        {
            if (hit.collider.gameObject.tag == "Player")
            {
                return true;
            }
        }
        if (Physics.Raycast(this.transform.position + Vector3.up * heightMultiplier, (this.transform.forward + this.transform.right).normalized, out hit, this.viewDistance))
        {
            if (hit.collider.gameObject.tag == "Player")
            {
                return true;
            }
        }
        if (Physics.Raycast(this.transform.position + Vector3.up * heightMultiplier, (this.transform.forward - this.transform.right).normalized, out hit, this.viewDistance))
        {
            if (hit.collider.gameObject.tag == "Player")
            {
                return true;
            }
        }
        return false;
    }


    /**
    * This method is used 
    * @return  distance positive if a guard detect the player
    *          distance negative if player is not detected  
    */
    public float DetectPlayer()
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
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, catchingRadius);
    }

    //TODO: METTERE NELLE GUARDIE
    /**
     * Animation methods
     */

    public void Walk()
    {
        setAnimBools(Mode.walking);
    }

    public void Run()
    {
        setAnimBools(Mode.running);
    }

    public void Idle()
    {
        setAnimBools(Mode.idle);
    }

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
