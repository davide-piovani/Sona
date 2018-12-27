using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformDoor : MonoBehaviour {

    public GameObject _player;
    public GameObject display;
    public float eulerX;
    public float eulerY;
    public float eulerZ;
    public float minDist;
    public float rotationSpeed;

    bool active = false;
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

        if ((_player.transform.position - display.transform.position).magnitude <= minDist)
        {
            active = true;
        }
        else {
            active = false;
        }

        if (Input.GetKeyDown(KeyCode.B) & active & (open | close)) {
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
