using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashableArea : MonoBehaviour {

    Material _normal;
    public Material _dash;
    GameController _gameController;

    void Start () {
        _normal = gameObject.GetComponent<Renderer>().material;
        _gameController = FindObjectOfType<GameController>();
	}
	
	void Update () {
        if (_gameController.GetActivePlayer().name.Equals("Charlie"))
        {
            gameObject.GetComponent<Renderer>().material = _dash;
        }
        else {
            gameObject.GetComponent<Renderer>().material = _normal;
        }
	}
}
