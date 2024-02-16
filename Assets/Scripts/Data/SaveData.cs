using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public OptionData OptionData;

    public SaveData(OptionData optionData)
    {
        OptionData = optionData;
    }

    public SaveData()
    {
        OptionData = new();
    }
}
