using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using UnityEngine.UI;

public class CombinationDisplay : MonoBehaviour {

    public float _minDistance = 2f;
    public GameObject _player;
    public GameObject _display;
    public Text _combinationString;
    public GameObject _combinationPad;
    public Text _pressKeyString;

    MonoBehaviour[] _playerScripts;

    bool closeToDisplay;
    OpenCloseManager _openCloseManager;
    

	void Start () {
        _combinationPad.SetActive(false);
        _playerScripts = _player.GetComponents<MonoBehaviour>();
        _openCloseManager = GetComponent<OpenCloseManager>();
        closeToDisplay = false;
    }
	
	void Update () {
        
        float dist = (_player.transform.position - _display.transform.position).magnitude;

        if (dist > 5)
        {
            closeToDisplay = false;
        }
        else
        {
            closeToDisplay = true;
        }


        if (closeToDisplay & dist <= _minDistance) {
            _pressKeyString.text = "Press 'B'";
        }

        if (closeToDisplay & dist > _minDistance)
        {
            _pressKeyString.text = "";
        }

        if (closeToDisplay & dist<=_minDistance & Input.GetKeyDown(KeyCode.B)) {
            _pressKeyString.text = "";
            _combinationPad.SetActive(true);
            ActiveOrDeActivePlayerScripts(false);
        }

        if (closeToDisplay & dist<=_minDistance & Input.GetKeyDown(KeyCode.Escape))
        {
            _pressKeyString.text = "";
            _combinationPad.SetActive(false);
            ActiveOrDeActivePlayerScripts(true);
        }

        if (closeToDisplay & dist<=_minDistance & _combinationString.text.Equals("7894")) {
            _pressKeyString.text = "";
            _combinationString.text = "";
            _combinationPad.SetActive(false);
            ActiveOrDeActivePlayerScripts(true);
            _openCloseManager.OpenCloseDoor(true);
        }

        if (closeToDisplay & dist <= _minDistance & _combinationString.text.Length==4 & !_combinationString.text.Equals("7894"))
        {
            _pressKeyString.text = "";
            _combinationString.text = "";
        }

    }

    void ActiveOrDeActivePlayerScripts(bool cond) {
        foreach (MonoBehaviour m in _playerScripts) {
            m.enabled = cond;
        }
    }
}
