using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GruntMovement : MonoBehaviour {

    [SerializeField] Transform[] path;

    //Grunt State
    bool playerInSight = false;
    Animator anim;

    //GruntPathFollowing
    int nextWaypoint = 0;
    bool increasingOrder = true;
    bool changeDirectionAtTheEndOfPath = true;
    float gruntSpeed = 1f;

    private void Start(){
        anim = GetComponent<Animator>();
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
        Vector3 currentPos = transform.position;
        Vector3 nextPos = path[nextWaypoint].position;

        if (currentPos != nextPos){
            transform.position = Vector3.MoveTowards(currentPos, nextPos, Time.deltaTime * gruntSpeed);
        } else {
            calculateNextWaypoint();
            rotateGrunt();
        }
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
