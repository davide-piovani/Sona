using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class GameSlot {

    public string name;
    public int number;
    public int sceneNumber;

    public GameSlot(int slotNumber) {
        name = "Saved Data " + slotNumber;
        number = slotNumber;
        sceneNumber = SceneManager.GetActiveScene().buildIndex;
    }
}
