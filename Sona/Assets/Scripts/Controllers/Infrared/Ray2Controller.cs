using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ray2Controller : MonoBehaviour, RayController {

    public float speed = 10f;
    
    bool moveUp;
    bool active;
    float midHeight;
    
    public float minHeight = 0.5f;
    public float maxHeight = 3.25f;

    void Start()
    {
        midHeight = (maxHeight + minHeight) / 2;

        if (transform.localPosition.y <= midHeight)
        {
            moveUp = true;
        }
        else {
            moveUp = false;
        }
        active = false;
    }

    void Update()
    {

        if (active)
        {

            float moveUpHeight = transform.localPosition.y + speed * TimeController.GetDelTaTime();
            float moveDownHeight = transform.localPosition.y - speed * TimeController.GetDelTaTime();

            if (moveUp & moveUpHeight > maxHeight)
            {
                moveUp = false;
            }
            else if (moveUp & moveUpHeight <= maxHeight)
            {
                transform.Translate(new Vector3(speed * TimeController.GetDelTaTime(), 0f, 0f));
            }
            else if (!moveUp & moveDownHeight < minHeight)
            {
                moveUp = true;
            }
            else if (!moveUp & moveDownHeight >= minHeight)
            {
                transform.Translate(new Vector3(-speed * TimeController.GetDelTaTime(), 0f, 0f));
            }

        }

    }

    public void ActiveInfraRedMovement()
    {
        active = true;
    }
}
