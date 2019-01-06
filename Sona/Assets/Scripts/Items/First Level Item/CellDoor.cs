using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellDoor : Interactable
{


    Key key;
    public float eulerX;
    public float eulerY;
    public float eulerZ;
    public float rotationSpeed;

    bool open = false;
    bool close = true;
    bool opening = false;
    Quaternion initialRotation;
    Quaternion targetRotation;
    Collider collider;
    DialogHelper dialogHelper;


    new void Start()
    {

        base.Start();
        initialRotation = transform.rotation;
        dialogHelper = FindObjectOfType<DialogHelper>();
        collider = GetComponent<Collider>();
        Vector3 dir = new Vector3(eulerX, eulerY, eulerZ);
        targetRotation = Quaternion.Euler(transform.rotation.eulerAngles + dir);
        key = FindObjectOfType<Key>();
        radius = 2f;
    }

    
    new void Update()
    {
        base.Update();
        if (!open & !close)
        {

            if (opening)
            {
                if (transform.rotation != targetRotation)
                {
                    transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
                    collider.enabled = false;
                }
                else
                {
                    open = true;
                }
            }
            else
            {
                if (transform.rotation != initialRotation)
                {
                    transform.rotation = Quaternion.Lerp(transform.rotation, initialRotation, rotationSpeed * Time.deltaTime);
                }
                else
                {
                    close = true;
                    collider.enabled = true;
                }
            }
        }
    }
    

    public override void Interact()
    {

        if (key.getKeyTaken() & (open | close) & getDistanceFromPlayer() < 2)
        {
            if (open)
            {
                open = false;
                opening = false;
            }
            else
            {
                close = false;
                opening = true;
                dialogHelper.TutorialDoorOpenPointReached();
            }
        }
        resetInteraction();

    }
}
