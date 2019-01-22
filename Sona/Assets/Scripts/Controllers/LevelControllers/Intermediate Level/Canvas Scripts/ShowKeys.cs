using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowKeys : MonoBehaviour {

    public GameObject _door1;
    public GameObject _door2;
    public Sprite _key;

    LockedDoor _lockDoor1;
    LockedDoor _lockDoor2;

    Image _image;
    GameController _gameController;

    void Start()
    {
        _lockDoor1 = _door1.GetComponentInChildren<LockedDoor>();
        _lockDoor2 = _door2.GetComponentInChildren<LockedDoor>();
        _gameController = FindObjectOfType<GameController>();
        _image = GetComponent<Image>();
        _image.enabled = false;
    }

    void Update() {
        if (!_lockDoor1.isKeyUsed() & _lockDoor1.isKeyOwned() & _gameController.GetActivePlayer().gameObject.name.Equals(_lockDoor1.KeyOwner()))
        {
            _image.sprite = _key;
            _image.enabled = true;
        }
        else if (!_lockDoor2.isKeyUsed() & _lockDoor2.isKeyOwned() & _gameController.GetActivePlayer().gameObject.name.Equals(_lockDoor2.KeyOwner()))
        {
            _image.sprite = _key;
            _image.enabled = true;
        }
        else
        {
            _image.enabled = false;
        }
    }

}
