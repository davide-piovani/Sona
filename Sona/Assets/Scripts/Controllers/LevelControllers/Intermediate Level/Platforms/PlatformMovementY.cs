using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlatformMovementY : MonoBehaviour, PlatformMovement {

    public float targetY;
    public float speed;

    bool isMoving = false;
    bool goOn = false;
    bool active = false;
    bool move = false;
    bool end = false;
    bool start = true;
    int playersOnPlatform = 0;

    Vector3 initialPosition;
    Vector3 endPosition;

    GameObject jack;
    GameObject hannah;
    GameObject charlie;
    float jackOffset;
    float hannahOffset;
    float charlieOffset;
    
    void Start()
    {
        initialPosition = transform.localPosition;
        endPosition = transform.localPosition + new Vector3(0, targetY, 0);
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
                Move(goOn);
            }
        }
    }

    void Move(bool cond)
    {

        if (cond & transform.localPosition != endPosition)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, endPosition, speed * TimeController.GetDelTaTime());
            if (playersOnPlatform > 0) {
                MovePlayers();
            }
            start = false;
        }
        else if (cond & transform.localPosition == endPosition)
        {
            if (playersOnPlatform  > 0) {
                MovePlayers();
            }
            isMoving = false;
            end = true;
        }
        else if (!cond & transform.localPosition != initialPosition)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, initialPosition, speed * TimeController.GetDelTaTime()); ;
            if (playersOnPlatform > 0) {
                MovePlayers();
            }
            end = false;
        }
        else if (!cond & transform.localPosition == initialPosition)
        {
            if (playersOnPlatform > 0) {
                MovePlayers();
            }
            isMoving = false;
            start = true;
        }

    }

    void ChangeDirection()
    {
        goOn = !goOn;
    }

    void MovePlayer(GameObject player, float offset) {
        float x = player.transform.position.x;
        float z = player.transform.position.z;
        player.transform.position = new Vector3(x, transform.position.y + offset, z);
    }

    void MovePlayers() {
        if (jack != null) {
            MovePlayer(jack, jackOffset);
        }
        if (hannah != null)
        {
            MovePlayer(hannah, hannahOffset);
        }
        if (charlie != null)
        {
            MovePlayer(charlie, charlieOffset);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playersOnPlatform++;

            if (collision.gameObject.name.Equals("Jack"))
            {
                
                jack = collision.gameObject;
                jackOffset = jack.transform.position.y - transform.localPosition.y;
                

            }
            else if (collision.gameObject.name.Equals("Hannah"))
            {
                hannah = collision.gameObject;
                hannahOffset = hannah.transform.position.y - transform.localPosition.y;
            }
            else if (collision.gameObject.name.Equals("Charlie"))
            {
                charlie = collision.gameObject;
                charlieOffset = charlie.transform.position.y - transform.localPosition.y;
            }
            else { }

        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playersOnPlatform--;

            if (collision.gameObject.name.Equals("Jack"))
            {
                jack = null;
                jackOffset = 0;

            }
            else if (collision.gameObject.name.Equals("Hannah"))
            {
                hannah = null;
                hannahOffset = 0;
            }
            else if (collision.gameObject.name.Equals("Charlie"))
            {
                charlie = null;
                charlieOffset = 0;
            }
            else { }
            
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

    public void CalculateOffsets() { }

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

    public int PlayersOnPlatform() {
        return playersOnPlatform;
    }

}
