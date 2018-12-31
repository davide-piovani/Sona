using System;
using UnityEngine;
using UnityEngine.AI;
using ApplicationConstants;

public class GuardMovement : MonoBehaviour {

    [SerializeField] Transform[] path;

    //Grunt State
    bool playerInSight = false;
    bool needToRestorePosition = false;
    Quaternion initialRotation;

    Animator anim;

    //GruntPathFollowing
    int nextWaypoint;
    bool increasingOrder = true;
    [SerializeField] bool changeDirectionAtTheEndOfPath = true;
    NavMeshAgent agent;

    private void Start(){
        anim = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();
        InitializeGuardMovement();
    }

    private void InitializeGuardMovement(){
        nextWaypoint = 0;
        if (path.Length > 1) {
            MoveGrunt(path[nextWaypoint].position, Mode.walking);
        } else {
            SetAnimBools(Mode.idle);
            if (path.Length == 0) SetGuardSpot();
        }
    }

    private void SetGuardSpot() {
        GameObject obj = new GameObject();
        obj.name = gameObject.name + " StartingPos";
        obj.transform.parent = transform.parent;
        obj.transform.position = transform.position;
        initialRotation = transform.rotation;
        path = new Transform[] { obj.transform };
    }

    // Update is called once per frame
    void Update () {
        if (!playerInSight) FollowPath();
	}

    public void FollowPlayer(bool follow, Vector3 destination){
        playerInSight = follow;
        if (follow) {
            MoveGrunt(destination, Mode.running);
            needToRestorePosition = true;
        }
    }

    private void FollowPath(){
        if (path.Length == 1) FollowOneWaypointPath();
        else FollowMultiWaypointsPath();
    }

    private void FollowOneWaypointPath(){
        if (needToRestorePosition && DestinationReached()){
            MoveGrunt(path[0].position, Mode.walking);
            needToRestorePosition = false;
        } else {
            if (DestinationReached()){
                SetAnimBools(Mode.idle);
                transform.rotation = initialRotation;
            } else {
                SetAnimBools(Mode.walking);
            }
        }
    }

    private void FollowMultiWaypointsPath(){
        if (DestinationReached()) {
            CalculateNextWaypoint();
            MoveGrunt(path[nextWaypoint].position, Mode.walking);
        }
    }

    private void MoveGrunt(Vector3 position, Mode mode){
        SetAnimBools(mode);
        agent.SetDestination(position);
    }

    private bool DestinationReached(){
        if (!agent.pathPending){
            if (agent.remainingDistance <= agent.stoppingDistance){
                if (!agent.hasPath || Math.Abs(agent.velocity.sqrMagnitude) < 0.001f){
                    return true;
                }
            }
        }
        return false;
    }

    private void CalculateNextWaypoint(){
        if (increasingOrder){
            if (nextWaypoint != path.Length - 1) {
                nextWaypoint++;
            } else {
                if (changeDirectionAtTheEndOfPath) {
                    increasingOrder = false;
                    nextWaypoint--;
                } else {
                    nextWaypoint = 0;
                }
            }
        } else {
            if (nextWaypoint != 0) {
                nextWaypoint--;
            } else {
                if (changeDirectionAtTheEndOfPath) {
                    increasingOrder = true;
                    nextWaypoint++;
                } else {
                    nextWaypoint = path.Length - 1;
                }
            }
        }
    }

    enum Mode{
        idle,
        walking,
        running
    }

    private void SetAnimBools(Mode mode){
        bool idle = false;
        bool walking = false;
        bool running = false;

        switch (mode){
            case Mode.idle:
                idle = true;
                break;
            case Mode.walking:
                walking = true;
                agent.speed = GuardConstants.guardWalkingSpeed;
                break;
            case Mode.running:
                running = true;
                agent.speed = GuardConstants.guardRunningSpeed;
                break;
        }

        anim.SetBool("isIdle", idle);
        anim.SetBool("isWalking", walking);
        anim.SetBool("isRunning", running);
    }
}
