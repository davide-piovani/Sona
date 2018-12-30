using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorController : MonoBehaviour {

    public ElevatorDoor bottomDoor;
    public ElevatorDoor topDoor;
    public DisplayActivator _bottomDisplay;
    public DisplayActivator _topDisplay;
    public InteractiveTextScript _platformText;

    PlatformMovement platform;
    bool playerClose = false;
    int state = 0;

    void Start () {
        platform = GetComponentInChildren<PlatformMovement>();
    }
	
	void Update () {
        ChangeState();
    }

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            playerClose = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerClose = false;
        }
    }

    void ChangeState() {

        if (state == 0)
        {

            if (Input.GetKeyDown(KeyCode.B))
            {

                if (_bottomDisplay.IsActive())
                {
                    _topDisplay.Necessary(false); /*forse da cambiare*/
                    _bottomDisplay.Necessary(false); /*forse da cambiare*/
                    platform.ActiveDeActivePlatform(true);
                    state = 1;
                }
                else if (_topDisplay.IsActive())
                {
                    _topDisplay.Necessary(false); /*forse da cambiare*/
                    _bottomDisplay.Necessary(false); /*forse da cambiare*/
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

            if (platform.IsPlayerOnPlatform())
            {
                _platformText.ShowText(); /* forse da cambiare */
            }
            else {
                _platformText.HideText(); /* forse da cambiare */
            }

            if (!_bottomDisplay.IsActive() & !platform.IsPlayerOnPlatform() & !playerClose) {
                bottomDoor.SlideDoor();
                state = 4;
            }
            else if (Input.GetKeyDown(KeyCode.B) & platform.IsPlayerOnPlatform() & bottomDoor.IsOpen()) {
                _platformText.HideText(); /* forse da cambiare */
                bottomDoor.SlideDoor();
                state = 5;
            }
            else { }

        }
        else if (state == 4)
        {
            if (bottomDoor.IsClose()){
                _topDisplay.Necessary(true); /*forse da cambiare*/
                _bottomDisplay.Necessary(true); /*forse da cambiare*/
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

            if (platform.IsPlayerOnPlatform())
            {
                _platformText.ShowText(); /* forse da cambiare */
            }
            else
            {
                _platformText.HideText(); /* forse da cambiare */
            }

            if (!_topDisplay.IsActive() & !platform.IsPlayerOnPlatform() & !playerClose)
            {
                topDoor.SlideDoor();
                state = 9;
            }
            else if (Input.GetKeyDown(KeyCode.B) & platform.IsPlayerOnPlatform() & topDoor.IsOpen())
            {
                _platformText.HideText(); /* forse da cambiare */
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
                _topDisplay.Necessary(true); /*forse da cambiare*/
                _bottomDisplay.Necessary(true); /*forse da cambiare*/
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
