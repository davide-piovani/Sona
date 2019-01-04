using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPlatform : MonoBehaviour {

    public DisplayActivator _display;

    Camera _platformCamera;
    PlatformMovement _platformMovement;
    PlatformCameraMovement _platformCameraScript;
    GameController _gameController;
    int state = 0;
    bool moving = false;
    
    void Start () {
        _gameController = FindObjectOfType<GameController>();
        _platformCamera = GetComponentInChildren<Camera>();
        _platformCameraScript = _platformCamera.GetComponentInChildren<PlatformCameraMovement>();
        _platformCamera.enabled = false;
        _platformMovement = GetComponentInChildren<PlatformMovement>();
	}
	
	void Update () {

        ChangeState();
    }

    void ChangeState() {

        if (state == 0)
        {
            if (Input.GetKeyDown(KeyCode.B) & _display.IsActive())
            {
                _gameController.PauseActive(false); /*prova per vedere se funziona*/
                _gameController.ChangePlayerActive(false); /*prova per vedere se funziona*/
                PlayerScriptsActive(false);
                _display.Necessary(false);
                _platformCamera.enabled = true;
                _platformCameraScript.MoveCamera();
                state = 1;
            }
        }
        else if (state == 1 & _platformCameraScript.IsArrived())
        {
            Invoke("MoveAnimation", 0.5f);
            state = 2;
        }
        else if (state == 2 & _platformMovement.IsMoving())
        {
            state = 3;
        }
        else if (state == 3 & !_platformMovement.IsMoving()) {
            Invoke("EndAnimation", 0.5f);
            state = 0;
        }
        else { }

    }

    void MoveAnimation() {
        _platformMovement.ActiveDeActivePlatform(true);
        _platformMovement.CalculateOffsets();
        _platformMovement.MovePlatform();
        moving = true;
    }

    void EndAnimation() {
        _platformMovement.ActiveDeActivePlatform(false);
        _platformCamera.enabled = false;
        PlayerScriptsActive(true);
        _platformCameraScript.ResetCamera();
        _display.Necessary(true);
        _gameController.PauseActive(true); /*prova per vedere se funziona*/
        _gameController.ChangePlayerActive(true); /*prova per vedere se funziona*/
    }

    void PlayerScriptsActive(bool cond) {
        GameObject _player = _display.GetPlayer();
        MonoBehaviour[] _playerScripts = _player.GetComponents<MonoBehaviour>();
        foreach (MonoBehaviour s in _playerScripts) {
            s.enabled = cond;
        }
        _player.GetComponentInChildren<Camera>().enabled = cond;
    }

}
