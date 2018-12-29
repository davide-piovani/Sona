using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlatformMovementZ : MonoBehaviour, PlatformMovement {

    public float targetZ;
    public float speed;

    bool isMoving = false;
    bool goOn = false;
    bool active = false;
    bool move = false;
    bool end = false;
    bool start = true;
    bool playerOnPlatform = false;
    float offset;

    Vector3 initialPosition;
    Vector3 endPosition;

    GameObject player;

    void Start()
    {
        initialPosition = transform.localPosition;
        endPosition = transform.localPosition + new Vector3(0, 0, targetZ);
    }


    void Update()
    {

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
                Move(goOn, player);
            }
        }
    }

    void Move(bool cond, GameObject player)
    {

        if (cond & transform.localPosition != endPosition)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, endPosition, speed * TimeController.GetDelTaTime());
            if (playerOnPlatform) {
                float x = player.transform.position.x;
                float y = player.transform.position.y;
                player.transform.position = new Vector3(x, y, transform.position.z + offset);
            }
            start = false;
        }
        else if (cond & transform.localPosition == endPosition)
        {
            if (playerOnPlatform) {
                float x = player.transform.position.x;
                float y = player.transform.position.y;
                player.transform.position = new Vector3(x, y, transform.position.z + offset);
            }
            isMoving = false;
            end = true;
        }
        else if (!cond & transform.localPosition != initialPosition)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, initialPosition, speed * TimeController.GetDelTaTime());
            if (playerOnPlatform) {
                float x = player.transform.position.x;
                float y = player.transform.position.y;
                player.transform.position = new Vector3(x, y, transform.position.z + offset);
            }
            end = false;
        }
        else if (!cond & transform.localPosition == initialPosition)
        {
            if (playerOnPlatform) {
                float x = player.transform.position.x;
                float y = player.transform.position.y;
                player.transform.position = new Vector3(x, y, transform.position.z + offset);
            }
            isMoving = false;
            start = true;
        }

    }

    void ChangeDirection()
    {
        goOn = !goOn;
    }


    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player = collision.gameObject;
            playerOnPlatform = true;
            offset = player.transform.position.z - transform.localPosition.z;
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

    public void ActiveDeActivePlatform(bool cond)
    {
        active = cond;
    }

    public void MovePlatform()
    {
        move = true;
    }

    public bool IsActive()
    {
        return active;
    }

    public bool IsMoving()
    {
        return isMoving;
    }

    public bool IsEnd()
    {
        return end;
    }

    public bool IsStart()
    {
        return start;
    }

    public bool IsPlayerOnPlatform()
    {
        return playerOnPlatform;
    }
}
