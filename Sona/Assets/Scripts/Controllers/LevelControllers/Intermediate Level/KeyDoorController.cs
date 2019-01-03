using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyDoorController : MonoBehaviour {

    public DisplayActivator _display;
    public float eulerX;
    public float eulerY;
    public float eulerZ;
    public float rotationSpeed;

    bool keyOwned = false;
    string keyOwner = "";

    bool active = false;
    bool open = false;
    bool close = true;
    bool opening = false;
    Quaternion initialRotation;
    Quaternion targetRotation;

    void Start()
    {
        initialRotation = transform.rotation;
        Vector3 dir = new Vector3(eulerX, eulerY, eulerZ);
        targetRotation = Quaternion.Euler(transform.rotation.eulerAngles + dir);
    }

    void Update()
    {
        if (!keyOwned | !_display.GetPlayer().name.Equals(keyOwner))
        {
            _display.Necessary(false);
            active = false;
        }
        else
        {
            _display.Necessary(true);
            active = true;
        }

        if (Input.GetKeyDown(KeyCode.B) & active & (open | close))
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
            }
        }

        if (!open & !close)
        {

            if (opening)
            {

                if (transform.rotation != targetRotation)
                {
                    transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
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
                }
            }
        }
    }

    public void KeyTaken(string name) {
        keyOwned = true;
        keyOwner = name;
    }


}
