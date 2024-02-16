using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public OptionData OptionData;
    public int Stage;
    public List<Item> MyItem;
    public int SuccessStack;

    public SaveData(OptionData optionData, int stage, List<Item> myItem, int successStack)
    {
        OptionData = optionData;
        Stage = stage;
        MyItem = myItem;
        SuccessStack = successStack;
    }

    public SaveData()
    {
        OptionData = new();
        Stage = 0;
        MyItem = new List<Item>();
        SuccessStack = 0;
    }
}
