using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombinationDoorController : MonoBehaviour {

    public DisplayActivator _displaySensor;
    public Text _combinationString;
    public GameObject _displayPad;
    ElevatorDoor _door;
    public string _combinationValue = "7894";
    int state = 0;
    
    void Start()
    {
        _displayPad.SetActive(false);
        _door = GetComponent<ElevatorDoor>();

    }

    void Update()
    {
        ChangeState();
    }

    void ChangeState() {

        if (state == 0 & Input.GetKeyDown(KeyCode.B) & _displaySensor.IsActive())
        {
            _displaySensor.Necessary(false);

            if (_door.IsClose())
            {
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
        else if (state == 1)
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
                _combinationString.text = "";
                _displayPad.SetActive(false);
                PlayerScriptsActive(true);
                state = 2;
            }
            else if (_combinationString.text.Equals(_combinationValue))
            {
                _combinationString.text = "";
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
                _displaySensor.Necessary(true);
                state = 0;
            }
        }
        else { }

    }

    void PlayerScriptsActive(bool cond)
    {
        GameObject _player = _displaySensor.GetPlayer();
        MonoBehaviour[] _playerScripts = _player.GetComponents<MonoBehaviour>();
        foreach (MonoBehaviour s in _playerScripts)
        {
            s.enabled = cond;
        }

    }
}
