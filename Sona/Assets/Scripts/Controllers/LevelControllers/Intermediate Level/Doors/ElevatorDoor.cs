using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorDoor : MonoBehaviour {

    public float speed;
    bool sliding = false;
    bool goUp = true;
    bool open = false;
    bool close = true;
    float scaleY;

    void Start () {
        scaleY = gameObject.transform.localScale.y;
	}
	
	void Update () {
        if (sliding) {
            Move();
        }
	}

    public void SlideDoor() {
        sliding = true;
    }

    public bool IsSliding() {
        return sliding;
    }

    public bool IsOpen()
    {
        return open;
    }

    public bool IsClose()
    {
        return close;
    }

    void Move() {

        if (goUp)
        {
            if (gameObject.transform.localScale.y >= 0)
            {
                scaleY -= speed * TimeController.GetDelTaTime();
                gameObject.transform.localScale = new Vector3(1, scaleY, 1);
                close = false;
            }
            else
            {
                scaleY = 0;
                gameObject.transform.localScale = new Vector3(1, scaleY, 1);
                goUp = false;
                sliding = false;
                open = true;
            }
        }
        else {
            if (gameObject.transform.localScale.y <= 1)
            {
                scaleY += speed * TimeController.GetDelTaTime();
                gameObject.transform.localScale = new Vector3(1, scaleY, 1);
                open = false;

            }
            else
            {
                scaleY = 1;
                gameObject.transform.localScale = new Vector3(1, scaleY, 1);
                goUp = true;
                sliding = false;
                close = true;
            }
        }
    }
}
