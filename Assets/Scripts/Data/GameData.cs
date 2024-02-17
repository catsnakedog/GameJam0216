using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public SaveData SaveData;
    public InGameData InGameData;
    public StageData StageData;
    public TextData TextData;
    public GameData()
    {
        SaveData = new();
        InGameData = new();
        StageData = new();
        TextData = new();
    }

    public GameData(SaveData saveData, InGameData inGameData, StageData stageData, TextData textData)
    {
        SaveData = saveData;
        InGameData = inGameData;
        StageData = stageData;
        TextData = textData;
    }
}
