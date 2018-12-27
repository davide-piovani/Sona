using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPlatform : MonoBehaviour {

    public GameObject _player;
    public GameObject display;
    public float minDist;
    Camera platformCamera;
    Camera playerCamera;
    PlatformMovement platform;
    PlatformCameraMovement _platformCameraScript;

    int state = 0;
    bool moving = false;
    bool active = false;

    void Start () {
        playerCamera = _player.GetComponentInChildren<Camera>();
        platformCamera = GetComponentInChildren<Camera>();
        _platformCameraScript = platformCamera.GetComponentInChildren<PlatformCameraMovement>();
        platformCamera.gameObject.SetActive(false);
        platform = GetComponentInChildren<PlatformMovement>();
	}
	
	void Update () {

        if ((_player.transform.position - display.transform.position).magnitude <= minDist)
        {
            active = true;
        }
        else {
            active = false;
        }

        ChangeState();
    }

    void ChangeState() {

        if (state == 0)
        {
            if (Input.GetKeyDown(KeyCode.B) & active)
            {
                PlayerScriptsActive(false);
                platformCamera.gameObject.SetActive(true);
                _platformCameraScript.MoveCamera();
                state = 1;
            }
        }
        else if (state == 1 & _platformCameraScript.IsArrived())
        {
            Invoke("MoveAnimation", 0.5f);
            state = 2;
        }
        else if (state == 2 & platform.IsMoving())
        {
            state = 3;
        }
        else if (state == 3 & !platform.IsMoving()) {
            Invoke("EndAnimation", 0.5f);
            state = 0;
        }
        else { }

    }

    void MoveAnimation() {
        platform.ActiveDeActivePlatform(true);
        platform.MovePlatform();
        moving = true;
    }

    void EndAnimation() {
        platform.ActiveDeActivePlatform(false);
        platformCamera.gameObject.SetActive(false);
        PlayerScriptsActive(true);
        _platformCameraScript.ResetCamera();
    }

    void PlayerScriptsActive(bool cond) {
        MonoBehaviour[] _playerScripts = _player.GetComponents<MonoBehaviour>();
        foreach (MonoBehaviour s in _playerScripts) {
            s.enabled = cond;
        }
        playerCamera.gameObject.SetActive(cond);
    }

}
