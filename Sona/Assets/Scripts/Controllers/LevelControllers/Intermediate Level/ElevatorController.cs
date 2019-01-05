using ApplicationConstants;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class ElevatorController : MonoBehaviour {

    public ElevatorDoor bottomDoor;
    public ElevatorDoor topDoor;
    public DisplayActivator _bottomDisplay;
    public DisplayActivator _topDisplay;
    public DisplayActivator _elevatorSensor;
    public InteractiveTextScript _platformText;

    PlatformMovement platform;
    int playersInElevator = 0;
    int state = 0;

    void Start () {
        platform = GetComponentInChildren<PlatformMovement>();
    }
	
	void Update () {
        ChangeState();
    }

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            playersInElevator++;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playersInElevator--;
        }
    }

    void ChangeState() {

        if (state == 0)
        {

            if (CrossPlatformInputManager.GetButtonDown(PlayersConstants.interactButton)/*Input.GetKeyDown(KeyCode.B)*/)
            {

                if (_bottomDisplay.IsActive())
                {
                    _topDisplay.Necessary(false);
                    _bottomDisplay.Necessary(false);
                    platform.ActiveDeActivePlatform(true);
                    state = 1;
                }
                else if (_topDisplay.IsActive())
                {
                    _topDisplay.Necessary(false);
                    _bottomDisplay.Necessary(false);
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

            if (platform.PlayersOnPlatform()==playersInElevator & playersInElevator!=0 & _elevatorSensor.IsActive())
            {
                _platformText.ShowText();
                if (CrossPlatformInputManager.GetButtonDown(PlayersConstants.interactButton)/*Input.GetKeyDown(KeyCode.B)*/ & bottomDoor.IsOpen()) {
                    _platformText.HideText();
                    bottomDoor.SlideDoor();
                    state = 5;
                }
            }
            else {
                _platformText.HideText();
            }

            if (!_bottomDisplay.IsActive() & platform.PlayersOnPlatform()==playersInElevator & playersInElevator==0) {
                bottomDoor.SlideDoor();
                state = 4;
            }

        }
        else if (state == 4)
        {
            if (bottomDoor.IsClose()){
                _topDisplay.Necessary(true);
                _bottomDisplay.Necessary(true);
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

            if (platform.PlayersOnPlatform()==playersInElevator & playersInElevator!=0)
            {
                _platformText.ShowText();
                if (CrossPlatformInputManager.GetButtonDown(PlayersConstants.interactButton)/*Input.GetKeyDown(KeyCode.B)*/ & topDoor.IsOpen()) {
                    _platformText.HideText();
                    topDoor.SlideDoor();
                    state = 8;
                }
            }
            else
            {
                _platformText.HideText();
            }

            if (!_topDisplay.IsActive() & platform.PlayersOnPlatform()==playersInElevator & playersInElevator==0) {
                topDoor.SlideDoor();
                state = 9;
            }
            

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
                _topDisplay.Necessary(true);
                _bottomDisplay.Necessary(true);
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
