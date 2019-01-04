using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StakeoutCam : MonoBehaviour {

    public GameObject target;
    public float rotationSpeed;
    public float speed;
    bool isOnStartPos = true;
    bool isOnEndPos = false;
    bool moving = false;
    bool moveOn = false;
    Vector3 initialPosition;
    Quaternion initialRotation;

    void Update()
    {
        //aggiungere movimento dx sx cam
        if (moving)
        {
            if (moveOn) {

                Turn(target.transform.rotation);
                Move(target.transform.position);

                if (transform.position == target.transform.position & transform.rotation == target.transform.rotation)
                {
                    isOnEndPos = true;
                    moving = false;
                }
            }
            else
            {

                Turn(initialRotation);
                Move(initialPosition);

                if (transform.position == initialPosition & transform.rotation == initialRotation)
                {
                    isOnStartPos = true;
                    moving = false;
                }
            }
        }
    }

    void Turn(Quaternion targetRotation)
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    void Move(Vector3 targetPosition)
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }

    public void SetPositionAndRotation() {
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }

    public void MoveCameraOn()
    {
        moving = true;
        moveOn = true;
        isOnEndPos = false;
        isOnStartPos = false;

    }

    public void MoveCameraBack()
    {
        moving = true;
        moveOn = false;
        isOnEndPos = false;
        isOnStartPos = false;

    }

    public bool IsOnStartPos() {
        return isOnStartPos;
    }

    public bool IsOnEndPos() {
        return isOnEndPos;
    }

}
