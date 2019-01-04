using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowKey : MonoBehaviour {

    public GameObject _door1;
    public GameObject _door2;
    public GameObject _combinationDoor;

    LockedDoor keyDoor1;
    LockedDoor keyDoor2;
    LockedDoor passpartout;

    Image _image;
    GameController _gameController;

    void Start () {
        keyDoor1 = _door1.GetComponentInChildren<LockedDoor>();
        keyDoor2 = _door2.GetComponentInChildren<LockedDoor>();
        passpartout = _combinationDoor.GetComponentInChildren<LockedDoor>();
        _gameController = FindObjectOfType<GameController>();
        _image = GetComponent<Image>();
        _image.enabled = false;
    }

    void Update () {

        if (!keyDoor1.isKeyUsed() & keyDoor1.isKeyOwned() & _gameController.GetActivePlayer().gameObject.name.Equals(keyDoor1.KeyOwner()))
        {
            //metti immagine chiave
            _image.enabled = true;
        }
        else if (!keyDoor2.isKeyUsed() & keyDoor2.isKeyOwned() & _gameController.GetActivePlayer().gameObject.name.Equals(keyDoor2.KeyOwner()))
        {
            //metti immagine chiave
            _image.enabled = true;
        }
        else if (!passpartout.isKeyUsed() & passpartout.isKeyOwned() & _gameController.GetActivePlayer().gameObject.name.Equals(passpartout.KeyOwner()))
        {
            //metti immagine passpartout
            _image.enabled = true;
        }
        else {
            _image.enabled = false;
        }

    }
}
