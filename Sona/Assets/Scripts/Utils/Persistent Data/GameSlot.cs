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

    public float JackPosX;
    public float JackPosY;
    public float JackPosZ;
    public float JackRotX;
    public float JackRotY;
    public float JackRotZ;
    public float JackRotW;

    public float HannahPosX;
    public float HannahPosY;
    public float HannahPosZ;
    public float HannahRotX;
    public float HannahRotY;
    public float HannahRotZ;
    public float HannahRotW;

    public float CharliePosX;
    public float CharliePosY;
    public float CharliePosZ;
    public float CharlieRotX;
    public float CharlieRotY;
    public float CharlieRotZ;
    public float CharlieRotW;

    public float musicVolume;
    public float effectsVolume;

    public GameSlot(int slotNumber) {
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
