using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformDoor : MonoBehaviour {

    public DisplayActivator _display;
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

        if (Input.GetKeyDown(KeyCode.B) & _display.IsActive() & (open | close)) {
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
                    close = true;
                }
            }
        }


    }
}
