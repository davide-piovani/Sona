using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlatformCameraMovement : MonoBehaviour {

    public GameObject target;
    public float rotationSpeed;
    public float speed;
    bool moving = false;
    bool arrived = false;
    Vector3 initialPosition;
    Quaternion initialRotation;

    void Start() {
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }


    void Update() {

        if (moving){

            Turn();
            Move();

            if (transform.position == target.transform.position) {
                arrived = true;
                moving = false;
            }
        }

    }

    void Turn() {
        transform.rotation = Quaternion.Lerp(transform.rotation, target.transform.rotation, rotationSpeed * Time.deltaTime);
    }

    void Move() {
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
    }

    public void MoveCamera() {
        moving = true;
    }

    public bool IsArrived() {
        return arrived;
    }

    public void ResetCamera() {
        transform.position = initialPosition;
        transform.rotation = initialRotation;
    }


}
