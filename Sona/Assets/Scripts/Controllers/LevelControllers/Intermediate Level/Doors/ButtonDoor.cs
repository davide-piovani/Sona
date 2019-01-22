using ApplicationConstants;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class ButtonDoor : MonoBehaviour {

    public InteractController _buttonInteractController;
    public InteractController _doorInteractController;
    public Camera _camera;
    public GameObject led;
    public Material locked;
    public Material unlocked;
    public float eulerX;
    public float eulerY;
    public float eulerZ;
    public float rotationSpeed;

    bool active = false;
    bool open = false;
    bool close = true;
    bool opening = false;
    Quaternion initialRotation;
    Quaternion targetRotation;

    void Start () {
        initialRotation = transform.rotation;
        Vector3 dir = new Vector3(eulerX, eulerY, eulerZ);
        targetRotation = Quaternion.Euler(transform.rotation.eulerAngles + dir);
        _doorInteractController.enabled = false;
        _camera.enabled = false;
        led.GetComponent<Renderer>().material = locked;
    }
	
	void Update () {

        if (_buttonInteractController.IsActive() & CrossPlatformInputManager.GetButtonDown(PlayersConstants.interactButton)) {
            _buttonInteractController.GetPlayer().GetComponentInChildren<Camera>().enabled = false;
            _buttonInteractController.Necessary(false);
            _camera.enabled = true;
            Invoke("UnlockDoor", 1f);
            Invoke("ActiveDoor", 3f);
        }

        if (CrossPlatformInputManager.GetButtonDown(PlayersConstants.interactButton) & _doorInteractController.IsActive() & active & (open | close))
        {
            _doorInteractController.Necessary(false);
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
                    _doorInteractController.Necessary(true);
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
                    _doorInteractController.Necessary(true);
                    close = true;
                }
            }
        }
    }

    void UnlockDoor() {
        led.GetComponent<Renderer>().material = unlocked;
    }

    void ActiveDoor() {
        active = true;
        _camera.enabled = false;
        _buttonInteractController.GetPlayer().GetComponentInChildren<Camera>().enabled = true;
        _buttonInteractController.ForceDeActive();
        //_doorInteractController.ReActive();
        _doorInteractController.enabled = true;
    }
}
