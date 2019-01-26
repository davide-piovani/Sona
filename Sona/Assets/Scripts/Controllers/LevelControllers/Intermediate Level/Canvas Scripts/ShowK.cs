using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowK : MonoBehaviour {

    public GameObject _door;
    public Sprite _key;

    LockedDoor _lockDoor;
    
    Image _image;
    GameController _gameController;

    void Start()
    {
        _lockDoor = _door.GetComponentInChildren<LockedDoor>();
        _gameController = FindObjectOfType<GameController>();
        _image = GetComponent<Image>();
        _image.enabled = false;
    }

    void Update()
    {
        if (!_lockDoor.isKeyUsed() & _lockDoor.isKeyOwned() & _gameController.GetActivePlayer().gameObject.name.Equals(_lockDoor.KeyOwner()))
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
