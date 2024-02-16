using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InGameData
{
    public List<Item> MyItem;
    public int SelectDoorIdx;

    public InGameData()
    {
        MyItem = new List<Item>();
        SelectDoorIdx = 0;
    }
    public InGameData(List<Item> myItem, int selectDoorIdx)
    {
        MyItem = myItem;
        SelectDoorIdx = selectDoorIdx;
    }
}