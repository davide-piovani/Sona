using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowKey : MonoBehaviour {

    public GameObject _door2;
    public GameObject _combinationDoor;
    public Sprite _key;
    public Sprite _electronicUnlocker;

    LockedDoor keyDoor2;
    LockedDoor passpartout;

    Image _image;
    GameController _gameController;

    void Start () {
        keyDoor2 = _door2.GetComponentInChildren<LockedDoor>();
        passpartout = _combinationDoor.GetComponentInChildren<LockedDoor>();
        _gameController = FindObjectOfType<GameController>();
        _image = GetComponent<Image>();
        _image.enabled = false;
    }

    void Update () {

        if (!keyDoor2.isKeyUsed() & keyDoor2.isKeyOwned() & _gameController.GetActivePlayer().gameObject.name.Equals(keyDoor2.KeyOwner()))
        {
            _image.sprite = _key;
            _image.enabled = true;
        }
        else if (!passpartout.isKeyUsed() & passpartout.isKeyOwned() & _gameController.GetActivePlayer().gameObject.name.Equals(passpartout.KeyOwner()))
        {
            _image.sprite = _electronicUnlocker;
            _image.enabled = true;
        }
        else {
            _image.enabled = false;
        }

    }
}
