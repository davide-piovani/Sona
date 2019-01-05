using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using ApplicationConstants;

[System.Serializable]
public class GameSlot {

    public string name;
    public int number;

    public int sceneNumber;

    public bool shouldRestorePos;
    public PlayerType activePlayer;

    public Vector3 JackPos;
    public Quaternion JackRotation;

    public Vector3 HannahPos;
    public Quaternion HannahRotation;

    public Vector3 CharliePos;
    public Quaternion CharlieRotation;

    public float musicVolume;
    public float effectsVolume;

    public GameSlot(int slotNumber) {
        name = "Saved Data " + slotNumber;
        number = slotNumber;

        sceneNumber = SceneManager.GetActiveScene().buildIndex;

        shouldRestorePos = false;

        musicVolume = GameConstants.defaultMusicVolume;
        effectsVolume = GameConstants.defaultEffectsVolume;
    }

    public void Save(){
        SaveSystem.SaveGameSlot(this);
    }
}
