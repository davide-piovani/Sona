using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.CrossPlatformInput;
using ApplicationConstants;

public class Stakeout : MonoBehaviour {

    public DisplayActivator _display;
    public GameObject _sensor1;
    public GameObject _sensor2;
    public float speed1 = 2f;
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
            if (CrossPlatformInputManager.GetButton(PlayersConstants.interactButton) /*Input.GetKey(KeyCode.C)*/ & _display.IsActive())
            {
                _gameController.PauseActive(false); /*prova per vedere se funziona*/
                _gameController.ChangePlayerActive(false); /*prova per vedere se funziona*/
                _display.Necessary(false);
                InitialSettings();
                MovePlayerTowardsWall();
                _cameraMovement.MoveCameraOn();
                state = 1;
            }
        }
        else if (state == 1)
        {
            if (!CrossPlatformInputManager.GetButton(PlayersConstants.interactButton)/*Input.GetKey(KeyCode.C)*/) {
                MovePlayerAwayFromWall();
                _cameraMovement.MoveCameraBack();
                state = 2;
            }
            
        }
        else if (state == 2 & _cameraMovement.IsOnStartPos() & !playerInMovement)
        {
            _gameController.PauseActive(true); /*prova per vedere se funziona*/
            _gameController.ChangePlayerActive(true); /*prova per vedere se funziona*/
            PlayerScriptsActive(true);
            WallCamActive(false);
            _display.Necessary(true);
            state = 0;
        }
        else { }

    }

    void InitialSettings() {
        SetInitialPositions();
        SetSensorToActive();
        SetCamPosAndRot();
        PlayerScriptsActive(false);
        WallCamActive(true);
    }

    void SetInitialPositions() {
        _initialPlayerPosition = _display.GetPlayer().transform.position;
        _initialPlayerRotation = _display.GetPlayer().GetComponentsInChildren<Transform>()[1].rotation;
        _initialPlayerCamPosition = _display.GetPlayer().gameObject.GetComponentInChildren<Camera>().transform.position;
        _initialPlayerCamRotation = _display.GetPlayer().gameObject.GetComponentInChildren<Camera>().transform.rotation;
    }

    void SetSensorToActive() {
        float x, y, z, dist1, dist2;

        x = _display.transform.position.x;
        y = _initialPlayerPosition.y;
        z = _display.transform.position.z;
        _display.transform.position = new Vector3(x, y, z);

        GameObject _player = _display.GetPlayer();
        dist1 = Vector3.Distance(_player.transform.position, _sensor1.transform.position);
        dist2 = Vector3.Distance(_player.transform.position, _sensor2.transform.position);

        if (dist1 <= dist2)
        {
            _playerTargetPosition = _sensor1.transform.position;
            _playerTargetRotation = _sensor1.transform.rotation;
        }
        else {
            _playerTargetPosition = _sensor2.transform.position;
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
        _player.GetComponent<NavMeshAgent>().enabled = cond;
        _player.GetComponent<Rigidbody>().useGravity = cond;
        _player.GetComponent<Collider>().enabled = cond;
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
