using ApplicationConstants;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyButtonScript : MonoBehaviour {

    public Sprite KEYBOARD_changeCharacter;
    public Sprite KEYBOARD_activePower;
    public Sprite KEYBOARD_interact;
    public Sprite XBOX_changeCharacter;
    public Sprite XBOX_activePower;
    public Sprite XBOX_interact;
    public Sprite PLAYSTATION_changeCharacter;
    public Sprite PLAYSTATION_activePower;
    public Sprite PLAYSTATION_interact;
    
    public Sprite GetChangeCharacterButtonImage() {
        if (GameSettings.GetControllerType().Equals(GameConstants.keyboardPad)) { return KEYBOARD_changeCharacter; }
        else if (GameSettings.GetControllerType().Equals(GameConstants.xboxPad)) { return XBOX_changeCharacter; }
        else if (GameSettings.GetControllerType().Equals(GameConstants.playStationPad)) { return PLAYSTATION_changeCharacter; }
        else { return null; }
    }

    public Sprite GetActivePowerButtonImage() {
        if (GameSettings.GetControllerType().Equals(GameConstants.keyboardPad)) { return KEYBOARD_activePower; }
        else if (GameSettings.GetControllerType().Equals(GameConstants.xboxPad)) { return XBOX_activePower; }
        else if (GameSettings.GetControllerType().Equals(GameConstants.playStationPad)) { return PLAYSTATION_activePower; }
        else { return null; }
    }

    public Sprite GetInteractButtonImage() {
        if (GameSettings.GetControllerType().Equals(GameConstants.keyboardPad)) { return KEYBOARD_interact; }
        else if (GameSettings.GetControllerType().Equals(GameConstants.xboxPad)) { return XBOX_interact; }
        else if (GameSettings.GetControllerType().Equals(GameConstants.playStationPad)) { return PLAYSTATION_interact; }
        else { return null;  }
    }


}
