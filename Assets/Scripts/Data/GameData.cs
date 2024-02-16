using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public SaveData SaveData;
    public InGameData InGameData;

    public GameData()
    {
        SaveData = new();
        InGameData = new();
    }

    public GameData(SaveData saveData, InGameData inGameData)
    {
        SaveData = saveData;
        InGameData = inGameData;
    }
}
