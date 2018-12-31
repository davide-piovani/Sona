using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayActivator : MonoBehaviour {

    public float radius = 2f;

    GameController _gameController;
    InteractiveTextScript _text;
    GameObject _player;
    bool active = false;
    bool necessary = true;

    void Start()
    {
        _gameController = FindObjectOfType<GameController>();
        _text = GetComponentInChildren<InteractiveTextScript>();
    }

    void Update()
    {
        Control();
        if (active & necessary)
        {
            _text.ShowText();
        }
        else {
            _text.HideText();
        }
    }

    public bool IsActive() {
        return active;
    }

    public void Necessary(bool cond) {
        necessary = cond;
    }

    public GameObject GetPlayer() {
        return _gameController.GetActivePlayer().gameObject;
    }

    void Control(){
        float distance = Vector3.Distance(_gameController.GetActivePlayer().transform.position, transform.position);
        if (distance <= radius)
        {
            active = true;
        }
        else {
            active = false;
        }
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
 

}
