using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using ApplicationConstants;

public class LastDoorScript : MonoBehaviour {

    GameController gameController;

	// Use this for initialization
	void Start () {
        gameController = FindObjectOfType<GameController>();
        initialRotation = transform.rotation;
        Vector3 dir = new Vector3(eulerX, eulerY, eulerZ);
        targetRotation = Quaternion.Euler(transform.rotation.eulerAngles + dir);
    }

    //public DisplayActivator _display;
    public float eulerX;
    public float eulerY;
    public float eulerZ;
    public float rotationSpeed;

    bool open = false;
    bool close = true;
    bool opening = false;
    Quaternion initialRotation;
    Quaternion targetRotation;

    void Update()
    {

        /*if (CrossPlatformInputManager.GetButtonDown(PlayersConstants.interactButton) & _display.IsActive() & (open | close) & gameController.lastDoorOpen)
        {
            //_display.Necessary(false);
            if (open)
            {
                open = false;
                opening = false;
            }
            else
            {
                close = false;
                opening = true;
            }
        }

        if (!open & !close)
        {

            if (opening)
            {

                if (transform.rotation != targetRotation)
                {
                    transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
                }
                else
                {
                    //_display.Necessary(true);
                    open = true;
                }
            }
            else
            {
                if (transform.rotation != initialRotation)
                {
                    transform.rotation = Quaternion.Lerp(transform.rotation, initialRotation, rotationSpeed * Time.deltaTime);
                }
                else
                {
                    //_display.Necessary(true);
                    close = true;
                }
            }
        }*/


    }
}
