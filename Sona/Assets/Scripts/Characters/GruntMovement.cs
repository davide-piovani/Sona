using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GruntMovement : MonoBehaviour {

    [SerializeField] Transform[] path;

    //Grunt State
    bool playerInSight = false;
    Animator anim;

    //GruntPathFollowing
    int nextWaypoint = 0;
    bool increasingOrder = true;
    bool changeDirectionAtTheEndOfPath = true;
    float gruntSpeed = 0.8f;
    NavMeshAgent agent;

    private void Start(){
        anim = GetComponentInChildren<Animator>();
        anim.speed = 1f;
        agent = GetComponent<NavMeshAgent>();
        moveGrunt(path[nextWaypoint].position);

    }

    // Update is called once per frame
    void Update () {
        if (playerInSight) {
            followPlayer();
        } else {
            followPath();
        }
	}

    private void followPlayer(){

    }

    private void followPath(){
        setAnimBools(Mode.walking);
        if (destinationReached()) {
            calculateNextWaypoint();
            moveGrunt(path[nextWaypoint].position);
        }


        /*Vector3 currentPos = transform.position;
        Vector3 nextPos = path[nextWaypoint].position;

        if (!checkIfEqualPos(currentPos, nextPos)){
            Vector3 towardPos = Vector3.MoveTowards(currentPos, nextPos, Time.deltaTime * gruntSpeed);
            //print("Step x: " + (towardPos.x - transform.position.x));
            //print("Step y: " + (towardPos.y - transform.position.y));
            //print("Step z: " + (towardPos.z - transform.position.z));
            transform.position = new Vector3(towardPos.x, transform.position.y, towardPos.z);
        } else {
            calculateNextWaypoint();
            rotateGrunt();
        }*/
    }

    private void moveGrunt(Vector3 position){
        //print("Moving grunt to this position: " + position);
        agent.SetDestination(position);
    }

    private bool destinationReached(){
        if (!agent.pathPending){
            if (agent.remainingDistance <= agent.stoppingDistance){
                if (!agent.hasPath || Math.Abs(agent.velocity.sqrMagnitude) < 0.001f){
                    return true;
                }
            }
        }
        return false;
    }

    private bool checkIfEqualPos(Vector3 pos1, Vector3 pos2){
        float epsilon = 0.001f;
        if (System.Math.Abs(pos1.x - pos2.x) < epsilon &&
            System.Math.Abs(pos1.z - pos2.z) < epsilon) return true;
        return false;
    }

    private void calculateNextWaypoint(){
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

    private void rotateGrunt(){
        Vector3 directionToNextWaypoint = path[nextWaypoint].transform.position - transform.position;
        transform.forward = directionToNextWaypoint;
    }

    enum Mode{
        idle,
        walking,
        running
    }

    private void setAnimBools(Mode mode){
        bool idle = false;
        bool walking = false;
        bool running = false;

        switch (mode){
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
