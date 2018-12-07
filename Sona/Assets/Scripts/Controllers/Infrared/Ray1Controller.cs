using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ray1Controller : MonoBehaviour, RayController {

    float turnSpeed = 100f;
    bool moveRight;
    bool changeDir;
    bool active;

    public float eulerMin;
    public float eulerMax;
    
    void Start() {

        if (transform.localPosition.x < 0)
        {
            moveRight = true;
        }
        else {
            moveRight = false;
        }

        changeDir = false;
        active = false;
        
    }

    void Update()
    {

        if (active) { 
            if (!changeDir & transform.localRotation.eulerAngles.y > eulerMin & transform.localRotation.eulerAngles.y < eulerMax) {
                changeDir = true;
                moveRight = !moveRight;
            }

            if (changeDir & ((moveRight & transform.localRotation.eulerAngles.y < eulerMin)| (!moveRight & transform.localRotation.eulerAngles.y > eulerMax))) {

                changeDir = false;
            }

            if (moveRight)
            {
                transform.Rotate(new Vector3(turnSpeed * TimeController.GetDelTaTime(), 0f, 0f));
            }
            else {
                transform.Rotate(new Vector3(-turnSpeed * TimeController.GetDelTaTime(), 0f, 0f));
            }
        }

    }

    public void ActiveInfraRedMovement() {
        active = true;
    }
}
