using ApplicationConstants;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class CombinationDoorController : MonoBehaviour, LockedDoor {

    public InteractController _interactSensor;
    public ButtonsController _combDisplay;
    InsertCombinationDigit _combinationString;
    ElevatorDoor _door;
    GameController _gameController;
    public string _combinationValue = "7894";

    bool keyUsed = false;
    bool keyOwned = false;
    string keyOwner = "";
    int state = 0;
    
    void Start()
    {
        _combinationString = FindObjectOfType<InsertCombinationDigit>();
        _gameController = FindObjectOfType<GameController>();
        _combDisplay.DisableInput();
        _combDisplay.gameObject.SetActive(false);
        _door = GetComponent<ElevatorDoor>();

    }

    void Update()
    {
        ChangeState();
    }

    void ChangeState() {

        if (state == 0 & GameSettings.GetButtonDown(PlayersConstants.interactButton) & _interactSensor.IsActive())
        {
            _interactSensor.Necessary(false);

            if (!keyUsed)
            {
                if (!keyOwned | !_interactSensor.GetPlayer().name.Equals(keyOwner))
                {
                    if (_door.IsClose())
                    {
                        _gameController.DisableInput(); /*prova per vedere se funziona*/
                        _combDisplay.gameObject.SetActive(true);
                        _combDisplay.ActiveInput();
                        _combDisplay.StartStringInsert();
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
            if (GameSettings.GetButtonDown(PlayersConstants.changeCharacterButton))
            {
                _combinationString.SetText("");
                _combDisplay.EndStringInsert();
                _combDisplay.DisableInput();
                _combDisplay.gameObject.SetActive(false);
                _gameController.ActiveInput(); /*prova per vedere se funziona*/
                PlayerScriptsActive(true);
                state = 2;
            }
            else if (_combinationString.GetText().Equals(_combinationValue))
            {
                _combinationString.SetText("");
                _combDisplay.EndStringInsert();
                _combDisplay.DisableInput();
                _combDisplay.gameObject.SetActive(false);
                _gameController.ActiveInput(); /*prova per vedere se funziona*/
                PlayerScriptsActive(true);
                _door.SlideDoor();
                state = 2;
            }
            else if (_combinationString.GetTextLenght() == 4 & !_combinationString.GetText().Equals(_combinationValue))
            {
                _combinationString.SetText("");
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
