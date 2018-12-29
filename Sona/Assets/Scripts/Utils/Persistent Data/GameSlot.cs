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

    public float musicVolume;
    public float effectsVolume;

    public GameSlot(int slotNumber) {
        name = "Saved Data " + slotNumber;
        number = slotNumber;
        sceneNumber = SceneManager.GetActiveScene().buildIndex;

        musicVolume = GameConstants.defaultMusicVolume;
        effectsVolume = GameConstants.defaultEffectsVolume;
    }

    public void SetVolume(float music, float effects){
        musicVolume = music;
        effectsVolume = effects;
    }

    public void Save(){
        SaveSystem.SaveGameSlot(this);
    }
}
