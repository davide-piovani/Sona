using ApplicationConstants;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class CombinationDoorController : MonoBehaviour, LockedDoor {

    public InteractController _interactSensor;
    public Text _combinationString;
    public GameObject _displayPad;
    ElevatorDoor _door;
    GameController _gameController;
    public string _combinationValue = "7894";

    bool keyUsed = false;
    bool keyOwned = false;
    string keyOwner = "";
    int state = 0;
    
    void Start()
    {
        _gameController = FindObjectOfType<GameController>();
        _displayPad.SetActive(false);
        _door = GetComponent<ElevatorDoor>();

    }

    void Update()
    {
        ChangeState();
    }

    void ChangeState() {

        if (state == 0 & CrossPlatformInputManager.GetButtonDown(PlayersConstants.interactButton) & _interactSensor.IsActive())
        {
            _interactSensor.Necessary(false);

            if (!keyUsed)
            {
                if (!keyOwned | !_interactSensor.GetPlayer().name.Equals(keyOwner))
                {
                    if (_door.IsClose())
                    {
                        _gameController.PauseActive(false); /*prova per vedere se funziona*/
                        _gameController.ChangePlayerActive(false); /*prova per vedere se funziona*/
                        _displayPad.SetActive(true);
                        PlayerScriptsActive(false);
                        state = 1;
                    }
                    else if (_door.IsOpen())
                    {
                        _door.SlideDoor();
                        state = 2;
                    }
                }
                else {
                    keyUsed = true;
                    _door.SlideDoor();
                    state = 2;
                }
            }
            else
            {
                _door.SlideDoor();
                state = 2;
            }

        }
        else if (state == 1)
        {
            if (CrossPlatformInputManager.GetButtonDown(PlayersConstants.interactButton))
            {
                _combinationString.text = "";
                _gameController.PauseActive(true); /*prova per vedere se funziona*/
                _gameController.ChangePlayerActive(true); /*prova per vedere se funziona*/
                _displayPad.SetActive(false);
                PlayerScriptsActive(true);
                state = 2;
            }
            else if (_combinationString.text.Equals(_combinationValue))
            {
                _combinationString.text = "";
                _gameController.PauseActive(true); /*prova per vedere se funziona*/
                _gameController.ChangePlayerActive(true); /*prova per vedere se funziona*/
                _displayPad.SetActive(false);
                PlayerScriptsActive(true);
                _door.SlideDoor();
                state = 2;
            }
            else if (_combinationString.text.Length == 4 & !_combinationString.text.Equals(_combinationValue))
            {
                _combinationString.text = "";
            }
        }
        else if (state == 2)
        {
            if (_door.IsOpen() | _door.IsClose()) {
                _interactSensor.Necessary(true);
                state = 0;
            }
        }
        else { }

    }

    void PlayerScriptsActive(bool cond)
    {
        GameObject _player = _interactSensor.GetPlayer();
        MonoBehaviour[] _playerScripts = _player.GetComponents<MonoBehaviour>();
        foreach (MonoBehaviour s in _playerScripts)
        {
            s.enabled = cond;
        }

    }

    public void KeyTaken(string name)
    {
        keyOwned = true;
        keyOwner = name;
    }

    public bool isKeyOwned()
    {
        return keyOwned;
    }

    public bool isKeyUsed()
    {
        return keyUsed;
    }

    public string KeyOwner()
    {
        return keyOwner;
    }
}
