using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorController : MonoBehaviour {

    public ElevatorDoor bottomDoor;
    public ElevatorDoor topDoor;
    public GameObject bottomDisplay;
    public GameObject topDisplay;
    public GameObject player;
    public float minDist;
    public float minDistToCloseDoor;
    PlatformMovement platform;
    bool bottomDisplayActive = false;
    bool topDisplayActive = false;

    int state = 0;
    
    void Start () {
        platform = GetComponentInChildren<PlatformMovement>();
	}
	
	void Update () {


        if ((player.transform.position - bottomDisplay.transform.position).magnitude <= minDist)
        {
            bottomDisplayActive = true;
        }
        else {
            bottomDisplayActive = false;
        }

        if ((player.transform.position - topDisplay.transform.position).magnitude <= minDist)
        {
            topDisplayActive = true;
        }
        else
        {
            topDisplayActive = false;
        }

        ChangeState();
        

    }

    void ChangeState() {

        if (state == 0)
        {

            if (Input.GetKeyDown(KeyCode.B))
            {

                if (bottomDisplayActive)
                {
                    platform.ActiveDeActivePlatform(true);
                    state = 1;
                }
                else if (topDisplayActive)
                {
                    platform.ActiveDeActivePlatform(true);
                    state = 10;
                }
                else { }

            }
        }
        else if (state == 1) {

            if (platform.IsEnd())
            {
                platform.MovePlatform();
                state = 2;
            }
            else if (platform.IsStart())
            {
                bottomDoor.SlideDoor();
                state = 3;
            }
            else { }

        }
        else if (state == 2)
        {

            if (platform.IsStart()){
                bottomDoor.SlideDoor();
                state = 3;
            }

        }
        else if (state == 3)
        {

            if (!bottomDisplayActive & !platform.IsPlayerOnPlatform() & (player.transform.position - bottomDisplay.transform.position).magnitude > minDistToCloseDoor) {
                bottomDoor.SlideDoor();
                state = 4;
            }
            else if (Input.GetKeyDown(KeyCode.B) & platform.IsPlayerOnPlatform() & bottomDoor.IsOpen()) {
                bottomDoor.SlideDoor();
                state = 5;
            }
            else { }

        }
        else if (state == 4)
        {
            if (bottomDoor.IsClose()){
                platform.ActiveDeActivePlatform(false);
                state = 0;
            }
        }
        else if (state == 5)
        {

            if (bottomDoor.IsClose()) {
                platform.MovePlatform();
                state = 6;
            }

        }
        else if (state == 6)
        {

            if (platform.IsEnd()) {
                topDoor.SlideDoor();
                state = 7;
            }

        }
        else if (state == 7)
        {

            if (!topDisplayActive & !platform.IsPlayerOnPlatform() & (player.transform.position - topDisplay.transform.position).magnitude > minDistToCloseDoor)
            {
                topDoor.SlideDoor();
                state = 9;
            }
            else if (Input.GetKeyDown(KeyCode.B) & platform.IsPlayerOnPlatform() & topDoor.IsOpen())
            {
                topDoor.SlideDoor();
                state = 8;
            }
            else { }

        }
        else if (state == 8)
        {

            if (topDoor.IsClose()) {
                platform.MovePlatform();
                state = 2;
            }

        }
        else if (state == 9)
        {

            if (topDoor.IsClose())
            {
                platform.ActiveDeActivePlatform(false);
                state = 0;
            }

        }
        else if (state == 10)
        {

            if (platform.IsStart())
            {
                platform.MovePlatform();
                state = 6;
            }
            else if (platform.IsEnd())
            {
                topDoor.SlideDoor();
                state = 7;
            }
            else { }

        }
    }
    
}
