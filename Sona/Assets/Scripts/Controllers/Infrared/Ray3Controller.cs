using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ray3Controller : MonoBehaviour, RayController {

    float turnSpeed = 100f;
    public GameObject target;

    bool moveRight;
    bool changeDir;
    bool active;

    float eulerRight = 30f;
    float eulerLeft = 330f;
    
    void Start()
    {

        moveRight = true;
        changeDir = false;
        active = false;

    }

    void Update()
    {

        if (active)
        {
            if (!changeDir & transform.eulerAngles.y > eulerRight & transform.eulerAngles.y < eulerLeft)
            {
                changeDir = true;
                moveRight = !moveRight;
            }

            if (changeDir & ((moveRight & transform.eulerAngles.y < eulerRight) | (!moveRight & transform.eulerAngles.y > eulerLeft)))
            {

                changeDir = false;
            }

            if (moveRight)
            {
                transform.RotateAround(target.transform.position, transform.right, turnSpeed * TimeController.GetDelTaTime());
            }
            else
            {
                transform.RotateAround(target.transform.position, -transform.right, turnSpeed * TimeController.GetDelTaTime());
            }
        }

    }

    public void ActiveInfraRedMovement()
    {
        active = true;
    }
}
