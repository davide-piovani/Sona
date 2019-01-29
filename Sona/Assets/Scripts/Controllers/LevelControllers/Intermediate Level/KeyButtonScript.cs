using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyButtonScript : MonoBehaviour {

    public Sprite KEYBOARD_changeCharacter;
    public Sprite KEYBOARD_activePower;
    public Sprite KEYBOARD_interact;

    //GameController _gameController;

    /*void Start() {
        _gameController = FindObjectOfType<GameController>();
    }*/


    public Sprite GetChangeCharacterButtonImage() {
        return KEYBOARD_changeCharacter;
    }

    public Sprite GetActivePowerButtonImage() {
        return KEYBOARD_activePower;
    }

    public Sprite GetInteractButtonImage() {
        return KEYBOARD_interact;
    }


}
