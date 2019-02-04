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
    int playersOnPlatform = 0;

    Vector3 initialPosition;
    Vector3 endPosition;

    GameObject jack;
    GameObject hannah;
    GameObject charlie;
    Vector3 jackOffset;
    Vector3 hannahOffset;
    Vector3 charlieOffset;


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
                Move(goOn);
            }
        }
    }

    void Move(bool cond)
    {

        if (cond & transform.localPosition != endPosition)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, endPosition, speed * TimeController.GetDelTaTime());
            if (playersOnPlatform > 0)
            {
                MovePlayers();
            }
            start = false;
        }
        else if (cond & transform.localPosition == endPosition)
        {
            if (playersOnPlatform > 0)
            {
                MovePlayers();
            }
            isMoving = false;
            end = true;
        }
        else if (!cond & transform.localPosition != initialPosition)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, initialPosition, speed * TimeController.GetDelTaTime());
            if (playersOnPlatform > 0)
            {
                MovePlayers();
            }
            end = false;
        }
        else if (!cond & transform.localPosition == initialPosition)
        {
            if (playersOnPlatform > 0)
            {
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

    void MovePlayer(GameObject player, Vector3 offset)
    {
        player.transform.position = transform.position + offset;
    }

    void MovePlayers()
    {
        if (jack != null)
        {
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
                //jackOffset = jack.transform.position - transform.position/*jack.transform.position.y - transform.localPosition.y*/;


            }
            else if (collision.gameObject.name.Equals("Hannah"))
            {
                hannah = collision.gameObject;
                //hannahOffset = hannah.transform.position - transform.position/*hannah.transform.position.y - transform.localPosition.y*/;
            }
            else if (collision.gameObject.name.Equals("Charlie"))
            {
                charlie = collision.gameObject;
                //charlieOffset = charlie.transform.position - transform.position/*charlie.transform.position.y - transform.localPosition.y*/;
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
                jackOffset = Vector3.zero;

            }
            else if (collision.gameObject.name.Equals("Hannah"))
            {
                hannah = null;
                hannahOffset = Vector3.zero;
            }
            else if (collision.gameObject.name.Equals("Charlie"))
            {
                charlie = null;
                charlieOffset = Vector3.zero;
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

    public void CalculateOffsets()
    {
        if (jack != null)
        {
            jackOffset = jack.transform.position - transform.position;
        }
        else if (hannah != null)
        {
            hannahOffset = hannah.transform.position - transform.position;
        }
        else if (charlie != null)
        {
            charlieOffset = charlie.transform.position - transform.position;
        }
        else { }
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

    public int PlayersOnPlatform()
    {
        return playersOnPlatform;
    }

}
