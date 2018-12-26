using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using ApplicationConstants;


public static class SaveSystem {
    private static BinaryFormatter formatter = new BinaryFormatter();
    private static string path;
    private static FileStream stream;


    public static void SaveGameSlot(GameSlot slot, int slotNumber) {
        path = GameConstants.gameSlotPath + slotNumber + GameConstants.gameExtension;

        stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, slot);
        stream.Close(); 
    }

    public static GameSlot LoadGameSlot(int slotNumber) {
        path = GameConstants.gameSlotPath + slotNumber + GameConstants.gameExtension;
        if (File.Exists(path)) {
            stream = new FileStream(path, FileMode.Open);

            GameSlot slot = formatter.Deserialize(stream) as GameSlot;

            stream.Close();
            return slot;
        } else {
            Debug.Log("Saved file not found in " + path);
            return null;
        }
    }

    public static GameSlot[] GetGameSlots(){
        GameSlot[] gameSlots = new GameSlot[3];
        for (int i = 0; i < 3; i++) {
            GameSlot slot = LoadGameSlot(i);
            if (slot != null) gameSlots[i] = slot;
        }
        return gameSlots;
    }
}
