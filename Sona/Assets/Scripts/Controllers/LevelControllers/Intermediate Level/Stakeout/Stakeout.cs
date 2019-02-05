using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.CrossPlatformInput;
using ApplicationConstants;

public class Stakeout : MonoBehaviour {

    public InteractController _display;
    public GameObject _sensor1;
    public GameObject _sensor2;
    public float speed1 = 50f;
    public float rotationSpeed = 50f;

    GameController _gameController;
    Camera _camera;
    StakeoutCam _cameraMovement;
    Vector3 _playerTargetPosition;
    Vector3 _initialPlayerPosition;
    Vector3 _initialPlayerCamPosition;
    Quaternion _playerTargetRotation;
    Quaternion _initialPlayerRotation;
    Quaternion _initialPlayerCamRotation;
    int state = 0;
    bool playerMoveTowardsWall = false;
    bool playerInMovement = false;
    bool leanLeft = false;

    void Start()
    {
        _gameController = FindObjectOfType<GameController>();
        _cameraMovement = GetComponentInChildren<StakeoutCam>();
        _camera = GetComponentInChildren<Camera>();
        _camera.enabled = false;
    }

    void Update()
    {
        ChangeState();

        if (playerInMovement)
        {
            if (playerMoveTowardsWall)
            {

                TurnPlayer(_playerTargetRotation);
                MovePlayer(_playerTargetPosition);

                if (_display.GetPlayer().transform.position == _playerTargetPosition)
                {
                    playerInMovement = false;
                }
            }
            else
            {
                TurnPlayer(_initialPlayerRotation);
                MovePlayer(_initialPlayerPosition);

                if (_display.GetPlayer().transform.position == _initialPlayerPosition)
                {
                    playerInMovement = false;
                }

            }
        }
    }

    void ChangeState()
    {
        if (state == 0)
        {
            if (CrossPlatformInputManager.GetButton(PlayersConstants.interactButton) & _display.IsActive())
            {
                _gameController.PauseActive(false); /*prova per vedere se funziona*/
                _gameController.ChangePlayerActive(false); /*prova per vedere se funziona*/
                _gameController.ManagePowerActive(false); /*prova per vedere se funziona*/
                _display.Necessary(false);
                InitialSettings();
                //MovePlayerTowardsWall();
                //_cameraMovement.MoveCameraOn();
                state = 1;
            }
        }
        else if (state == 1 & CrossPlatformInputManager.GetButton(PlayersConstants.interactButton) & _display.GetPlayer().GetComponent<Animator>().GetBool("isIdle")) {
            PlayerLean(true); /* ANIMATION */
            MovePlayerTowardsWall();
            _cameraMovement.MoveCameraOn();
            state = 2;
        }
        else if (state == 2)
        {

            if (!CrossPlatformInputManager.GetButton(PlayersConstants.interactButton)) {
                PlayerLean(false); /* ANIMATION */
                MovePlayerAwayFromWall();
                _cameraMovement.MoveCameraBack();
                state = 3;
            }

        }
        else if (state == 3)
        //else if (state == 3 & _cameraMovement.IsOnStartPos() & !playerInMovement)
        {
            _gameController.PauseActive(true); /*prova per vedere se funziona*/
            _gameController.ChangePlayerActive(true); /*prova per vedere se funziona*/
            _gameController.ManagePowerActive(true); /*prova per vedere se funziona*/
            PlayerScriptsActive(true);
            WallCamActive(false);
            _display.Necessary(true);
            state = 0;
        }
        else { }

    }

    void InitialSettings() {
        PlayerScriptsActive(false);
        PlayerInIdle(); /* ANIMATION */
        SetInitialPositions();
        SetSensorToActive();
        SetCamPosAndRot();
        WallCamActive(true);
        //PlayerLean(true); /* ANIMATION */
    }

    void SetInitialPositions() {
        _initialPlayerPosition = _display.GetPlayer().transform.position;
        _initialPlayerRotation = _display.GetPlayer().GetComponentsInChildren<Transform>()[1].rotation;
        _initialPlayerCamPosition = _display.GetPlayer().gameObject.GetComponentInChildren<Camera>().transform.position;
        _initialPlayerCamRotation = _display.GetPlayer().gameObject.GetComponentInChildren<Camera>().transform.rotation;
    }

    void PlayerInIdle() {
        if (_display.GetPlayer().GetComponent<Animator>().GetBool("isRunning"))
        {
            _display.GetPlayer().GetComponent<Animator>().SetBool("isRunning", false);
        }
        if (_display.GetPlayer().GetComponent<Animator>().GetBool("isWalking"))
        {
            _display.GetPlayer().GetComponent<Animator>().SetBool("isWalking", false);
        }
        if (!_display.GetPlayer().GetComponent<Animator>().GetBool("isIdle")) {
            _display.GetPlayer().GetComponent<Animator>().SetBool("isIdle", true);
        }
    }

    void PlayerLean(bool cond) {
        _display.GetPlayer().GetComponent<Animator>().SetBool("isIdle", !cond);
        if (leanLeft) {
            _display.GetPlayer().GetComponent<Animator>().SetBool("isLeanLeft", cond);
        }
        else {
            _display.GetPlayer().GetComponent<Animator>().SetBool("isLeanRight", cond);
        }
    }

    void SetSensorToActive() {
        float x, y, z, dist1, dist2;

        x = _display.transform.position.x;
        y = _initialPlayerPosition.y;
        z = _display.transform.position.z;
        //_display.transform.position = new Vector3(x, y, z);

        GameObject _player = _display.GetPlayer();
        dist1 = Vector3.Distance(_player.transform.position, _sensor1.transform.position);
        dist2 = Vector3.Distance(_player.transform.position, _sensor2.transform.position);

        if (dist1 <= dist2)
        {
            leanLeft = true;
            _playerTargetPosition = new Vector3(_sensor1.transform.position.x, _initialPlayerPosition.y, _sensor1.transform.position.z);
            _playerTargetRotation = _sensor1.transform.rotation;
        }
        else {
            leanLeft = false;
            _playerTargetPosition = new Vector3(_sensor2.transform.position.x, _initialPlayerPosition.y, _sensor2.transform.position.z);
            _playerTargetRotation = _sensor2.transform.rotation;
        }

    }

    void SetCamPosAndRot() {
        _camera.transform.position = _initialPlayerCamPosition;
        _camera.transform.rotation = _initialPlayerCamRotation;
        _cameraMovement.SetPositionAndRotation();
    }

    void WallCamActive(bool cond) {
        _camera.enabled = cond;
    }

    void PlayerScriptsActive(bool cond)
    {
        GameObject _player = _display.GetPlayer();
        MonoBehaviour[] _playerScripts = _player.GetComponents<MonoBehaviour>();
        foreach (MonoBehaviour s in _playerScripts)
        {
            s.enabled = cond;
        }

        //_player.GetComponent<Rigidbody>().useGravity = cond;
        //_player.GetComponent<Collider>().enabled = cond;
        _player.GetComponentInChildren<Camera>().enabled = cond;
    }

    void TurnPlayer(Quaternion targetRotation)
    {
        _display.GetPlayer().GetComponentsInChildren<Transform>()[1].rotation = 
            Quaternion.Lerp(_display.GetPlayer().GetComponentsInChildren<Transform>()[1].rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    void MovePlayer(Vector3 targetPosition)
    {
        _display.GetPlayer().transform.position = Vector3.MoveTowards(_display.GetPlayer().transform.position, targetPosition, speed1 * Time.deltaTime);
    }

    void MovePlayerTowardsWall() {
        playerMoveTowardsWall = true;
        playerInMovement = true;
    }

    void MovePlayerAwayFromWall() {
        playerMoveTowardsWall = false;
        playerInMovement = true;
    }

}
