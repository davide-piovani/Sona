using ApplicationConstants;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class KeyDoorController : MonoBehaviour, LockedDoor {

    public InteractController _interact;
    public float eulerX;
    public float eulerY;
    public float eulerZ;
    public float rotationSpeed;

    bool keyUsed = false;
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
        if (!keyUsed)
        {
            if (!keyOwned | !_interact.GetPlayer().name.Equals(keyOwner))
            {
                _interact.Necessary(false);
                active = false;
            }
            else
            {
                _interact.Necessary(true);
                active = true;
            }
        }
        else {
            active = true;
        }

        if (GameSettings.GetButtonDown(PlayersConstants.interactButton) & _interact.IsActive() & active & (open | close))
        {
            keyUsed = true;
            _interact.Necessary(false);
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
                    _interact.Necessary(true);
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
                    _interact.Necessary(true);
                    close = true;
                }
            }
        }
    }

    public void KeyTaken(string name) {
        keyOwned = true;
        keyOwner = name;
    }

    public bool isKeyOwned() {
        return keyOwned;
    }

    public bool isKeyUsed() {
        return keyUsed;
    }

    public string KeyOwner() {
        return keyOwner;
    }

}
