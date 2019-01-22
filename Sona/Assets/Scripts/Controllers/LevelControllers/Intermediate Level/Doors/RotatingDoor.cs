using ApplicationConstants;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class RotatingDoor : MonoBehaviour {

    public InteractController _interact;
    public float eulerX;
    public float eulerY;
    public float eulerZ;
    public float rotationSpeed;

    bool open = false;
    bool close = true;
    bool opening = false;
    Quaternion initialRotation;
    Quaternion targetRotation;

    void Start () {
        initialRotation = transform.rotation;
        Vector3 dir = new Vector3(eulerX, eulerY, eulerZ);
        targetRotation = Quaternion.Euler(transform.rotation.eulerAngles + dir);
    }
	
	void Update () {

        if (CrossPlatformInputManager.GetButtonDown(PlayersConstants.interactButton) & _interact.IsActive() & (open | close)) {
            _interact.Necessary(false);
            if (open)
            {
                open = false;
                opening = false;
            }
            else {
                close = false;
                opening = true;
            }
        }

        if (!open & !close) {

            if (opening) {

                if (transform.rotation != targetRotation)
                {
                    transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
                }
                else {
                    _interact.Necessary(true);
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
                    _interact.Necessary(true);
                    close = true;
                }
            }
        }


    }
}
