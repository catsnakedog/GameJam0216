using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InGameData
{
    public List<Item> MyItem;
    public int SelectDoorIdx;
    public int Stage;

    public InGameData()
    {
        MyItem = new List<Item>();
        SelectDoorIdx = 0;
        Stage = 0;
    }
    public InGameData(List<Item> myItem, int selectDoorIdx, int stage)
    {
        MyItem = myItem;
        SelectDoorIdx = selectDoorIdx;
        Stage = stage;
    }
}