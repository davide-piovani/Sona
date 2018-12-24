using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour {

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

            if (platform.isTop())
            {
                platform.MovePlatform();
                state = 2;
            }
            else if (platform.isBottom())
            {
                bottomDoor.SlideDoor();
                state = 3;
            }
            else { }

        }
        else if (state == 2)
        {

            if (platform.isBottom()){
                bottomDoor.SlideDoor();
                state = 3;
            }

        }
        else if (state == 3)
        {

            if (!bottomDisplayActive & !platform.isPlayerOnPlatform() & (player.transform.position - bottomDisplay.transform.position).magnitude > minDistToCloseDoor) {
                bottomDoor.SlideDoor();
                state = 4;
            }
            else if (Input.GetKeyDown(KeyCode.B) & platform.isPlayerOnPlatform() & bottomDoor.isOpen()) {
                bottomDoor.SlideDoor();
                state = 5;
            }
            else { }

        }
        else if (state == 4)
        {
            if (bottomDoor.isClose()){
                platform.ActiveDeActivePlatform(false);
                state = 0;
            }
        }
        else if (state == 5)
        {

            if (bottomDoor.isClose()) {
                platform.MovePlatform();
                state = 6;
            }

        }
        else if (state == 6)
        {

            if (platform.isTop()) {
                topDoor.SlideDoor();
                state = 7;
            }

        }
        else if (state == 7)
        {

            if (!topDisplayActive & !platform.isPlayerOnPlatform() & (player.transform.position - topDisplay.transform.position).magnitude > minDistToCloseDoor)
            {
                topDoor.SlideDoor();
                state = 9;
            }
            else if (Input.GetKeyDown(KeyCode.B) & platform.isPlayerOnPlatform() & topDoor.isOpen())
            {
                topDoor.SlideDoor();
                state = 8;
            }
            else { }

        }
        else if (state == 8)
        {

            if (topDoor.isClose()) {
                platform.MovePlatform();
                state = 2;
            }

        }
        else if (state == 9)
        {

            if (topDoor.isClose())
            {
                platform.ActiveDeActivePlatform(false);
                state = 0;
            }

        }
        else if (state == 10)
        {

            if (platform.isBottom())
            {
                platform.MovePlatform();
                state = 6;
            }
            else if (platform.isTop())
            {
                topDoor.SlideDoor();
                state = 7;
            }
            else { }

        }
    }
    
}
