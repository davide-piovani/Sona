using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour {

    public float maxHeight;
    public float minHeight;
    public float speed;

    bool isMoving = false;
    bool goUp = false;
    bool active = false;
    bool move = false;
    bool top = false;
    bool bottom = true;
    bool playerOnPlatform = false;
    float x;
    float z;

    GameObject player;

    void Start()
    {
        x = gameObject.transform.localPosition.x;
        z = gameObject.transform.localPosition.z;
    }


    void Update () {

        if (active)
        {
            if (!isMoving & move)
            {
                ChangeDirection();
                isMoving = true;
                move = false;
            }

            if (isMoving)
            {
                Move(goUp, player);
            }
        }
    }

    void Move(bool cond, GameObject player) {

        if (cond & gameObject.transform.localPosition.y <= maxHeight)
        {
            gameObject.transform.Translate(Vector3.up * speed * TimeController.GetDelTaTime());
            player.transform.Translate(Vector3.up * speed * TimeController.GetDelTaTime());
            bottom = false;
        }
        else if (cond & gameObject.transform.localPosition.y >= maxHeight)
        {
            gameObject.transform.localPosition = new Vector3(x ,maxHeight, z);
            isMoving = false;
            top = true;
        }
        else if (!cond & gameObject.transform.localPosition.y >= minHeight)
        {
            gameObject.transform.Translate(-Vector3.up * speed * TimeController.GetDelTaTime());
            player.transform.Translate(-Vector3.up * speed * TimeController.GetDelTaTime());
            top = false;
        }
        else if (!cond & gameObject.transform.localPosition.y <= minHeight)
        {
            gameObject.transform.localPosition = new Vector3(x, minHeight, z);
            isMoving = false;
            bottom = true;
        }

    }

    void ChangeDirection() {
        goUp = !goUp;
    }


    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player")) {
            player = collision.gameObject;
            playerOnPlatform = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player = null;
            playerOnPlatform = false;
        }
    }

    public void ActiveDeActivePlatform(bool cond) {
        active = cond;
    }

    public void MovePlatform() {
        move = true;
    }

    public bool IsActive() {
        return active;
    }

    public bool IsMoving() {
        return isMoving;
    }

    public bool isTop() {
        return top;
    }

    public bool isBottom() {
        return bottom;
    }

    public bool isPlayerOnPlatform() {
        return playerOnPlatform;
    }
   
}
