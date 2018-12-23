using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour {

    public float maxHeight;
    public float minHeight;
    public float speed;

    bool isMoving = false;
    bool up = false;
    float x;
    float z;

    GameObject player;

    void Start()
    {
        x = gameObject.transform.localPosition.x;
        z = gameObject.transform.localPosition.z;
    }


    void Update () {

        if (!isMoving & Input.GetKeyDown(KeyCode.B)) {
            ChangeDirection();
            isMoving = true;
        }

        if (isMoving){
            Move(up, player);
        }
    }

    void Move(bool cond, GameObject player) {

        if (cond & gameObject.transform.localPosition.y <= maxHeight)
        {
            gameObject.transform.Translate(Vector3.up * speed * TimeController.GetDelTaTime());
            player.transform.Translate(Vector3.up * speed * TimeController.GetDelTaTime());
        }
        else if (cond & gameObject.transform.localPosition.y >= maxHeight)
        {
            gameObject.transform.localPosition = new Vector3(x ,maxHeight, z);
            isMoving = false;
        }
        else if (!cond & gameObject.transform.localPosition.y >= minHeight)
        {
            gameObject.transform.Translate(-Vector3.up * speed * TimeController.GetDelTaTime());
            player.transform.Translate(-Vector3.up * speed * TimeController.GetDelTaTime());
        }
        else if (!cond & gameObject.transform.localPosition.y <= minHeight)
        {
            gameObject.transform.localPosition = new Vector3(x, minHeight, z);
            isMoving = false;
        }

    }

    void ChangeDirection() {
        up = !up;
    }


    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player")) {
            player = collision.gameObject;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player = null;
        }
    }
   
}
