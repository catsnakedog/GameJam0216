using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public SaveData SaveData;
    public InGameData InGameData;
    public StageData StageData;
    public GameData()
    {
        SaveData = new();
        InGameData = new();
        StageData = new();
    }

    public GameData(SaveData saveData, InGameData inGameData, StageData stageData)
    {
        SaveData = saveData;
        InGameData = inGameData;
        StageData = stageData;
    }
}
