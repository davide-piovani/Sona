using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using ApplicationConstants;

public class GuardController : MonoBehaviour {

    [HideInInspector] public float lookRadius;
    float distance;

    Decision decision;

    Animator anim;
    GameObject allarm;
    GuardGroup guardsGroup;

    [HideInInspector] public Transform initialPosition;
    public Action currentAction;
    //Action lastAction;
    [HideInInspector] public bool actionCompleted = true;
    [HideInInspector] public Vector3 lastPlayerPos;

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
        lookRadius = GuardConstants.guardLookRadius;
        viewDistance = lookRadius;
        catchingRadius = GuardConstants.guardCatchingRadius;
        GuardGroup guardGroup = GetComponentInParent<GuardGroup>();
        if (guardGroup.allarm != null)
        {
            allarmTransform = guardGroup.allarm.transform;
        }
        SetGuardSpot();
        //initialPosition = transform.position;
        //initialDirection = transform.rotation;
    }

    private void SetGuardSpot(){
        GameObject obj = new GameObject();
        obj.name = gameObject.name + " StartingPos";
        obj.transform.parent = transform.parent;
        obj.transform.position = transform.position;
        obj.transform.rotation = transform.rotation;
        initialPosition = obj.transform;
    }

    public void SetLastTarget(){
        GameObject obj = GameObject.Find(gameObject.name + " lastTargetPos");
        if (!obj){
            obj = new GameObject();
            obj.name = gameObject.name + " lastTargetPos";
            obj.transform.parent = transform.parent;
        }
        if (actionCompleted) obj.transform.position = target.position;
        lastTarget = obj.transform;
    }

    // Update is called once per frame
    void Update()
    {
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
        return gameController.GetActivePlayer().IsVisible();
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
        agent.enabled = true;
        //setAnimBools(Mode.running);
        agent.SetDestination(transform.position);
    }
    
    public void GuardCatchPlayer()
    {
        if (DetectPlayer() < 1.8f && DetectPlayer() > 0)
        {
            gameController.PlayerCatched();
        }

    }

    public void RestoreInitialRotation(){
        transform.rotation = initialPosition.rotation;
    }

    /**
    * This method is used by guards to find the player
    */
    public bool Investigate()
    {
        //Transform target = null;
        //timer = Time.deltaTime;
        //RaycastHit hit;
        //visual rappresentation of this ray
        //Debug.DrawRay(this.transform.position + Vector3.up * heightMultiplier, this.transform.forward * viewDistance, Color.green);
        //Debug.DrawRay(this.transform.position + Vector3.up * heightMultiplier, (this.transform.forward + transform.right).normalized * this.viewDistance, Color.green);
        //Debug.DrawRay(this.transform.position + Vector3.up * heightMultiplier, (this.transform.forward - transform.right).normalized * this.viewDistance, Color.green);

        //Look for player in the three directions 
        Vector3 startPos = transform.position + new Vector3(0, 1.5f, 0);
        Vector3 targetPos = target.transform.position + new Vector3(0, 0.5f, 0);
        Debug.DrawRay(startPos, targetPos - startPos, Color.green);

        bool playerDetected = false;
        float distanceToPlayer = Vector3.Distance(targetPos, startPos);
        RaycastHit[] hits = Physics.RaycastAll(startPos, targetPos - startPos, distanceToPlayer);
        int collisions = 0;
        foreach (RaycastHit hit in hits){
            Player player = hit.collider.gameObject.GetComponent<Player>();
            if (player){
                if (!PlayerIsInSightAngle(player)) return false;
                playerDetected = true;
            }
            if (isInGuardSightCollisionLayers(hit.collider.gameObject.layer)) collisions++;
        }
        return (collisions == 1 && playerDetected);

    }

    private bool isInGuardSightCollisionLayers(int layer){
        foreach (int guardSightLayer in GuardConstants.guardSightCollisionLayers){
            if (layer == guardSightLayer) return true;
        }
        return false;
    }

    private bool PlayerIsInSightAngle(Player player){
        return (CalculateAngle(player) <= GuardConstants.guardVisionAngle);
    }

    private float CalculateAngle(Player player)
    {
        Vector3 directionToPlayer = player.transform.position - transform.position;
        return Vector3.Angle(directionToPlayer, transform.forward);
    }


    /**
    * This method is used 
    * @return  distance positive if a guard detect the player
    *          distance negative if player is not detected  
    */
    public float DetectPlayer()
    {
        target = gameController.GetActivePlayer().transform;
        distance = Vector3.Distance(target.position, transform.position);
        if (distance <= lookRadius)
        {
            return distance;
        }
        return -1;
    }

    /**
     * This method is used when a guard reachs the player. Player was catch and the game ends
     */
    public Vector3 getInitialPosition()
    {
        return initialPosition.position;
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
        agent.speed = GuardConstants.guardWalkingSpeed * TimeController.GetDelTaTime();
        anim.speed = 10 * TimeController.GetDelTaTime();
    }

    public void Run()
    {
        setAnimBools(Mode.running);
        agent.speed = GuardConstants.guardRunningSpeed * TimeController.GetDelTaTime();
        anim.speed = 10 * TimeController.GetDelTaTime();
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
