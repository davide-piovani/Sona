using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenDoor : MonoBehaviour {

    public float _minDistance = 2f;
    public GameObject _player;
    public GameObject _display;
    public Text _pressKeyString;

    bool closeToDisplay;
    OpenCloseManager _openCloseManager;

    void Start()
    {
        closeToDisplay = false;
        _openCloseManager = GetComponent<OpenCloseManager>();
    }

    void Update()
    {
        if (_display)
        {
            float dist = (_player.transform.position - _display.transform.position).magnitude;

            if (dist > 5)
            {
                closeToDisplay = false;
            }
            else
            {
                closeToDisplay = true;
            }

            if (closeToDisplay & dist <= _minDistance)
            {
                _pressKeyString.text = "Press 'B'";
            }

            if (closeToDisplay & dist > _minDistance)
            {
                _pressKeyString.text = "";
            }

            if (closeToDisplay & dist <= _minDistance & Input.GetKeyDown(KeyCode.B))
            {
                _pressKeyString.text = "";
                _openCloseManager.OpenCloseDoor(true);
            }
        }
    }
}
