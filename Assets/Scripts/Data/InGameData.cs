using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InGameData
{
    public List<Item> MyItem;

    public InGameData()
    {
        MyItem = new List<Item>();
    }
    public InGameData(List<Item> myItem)
    {
        MyItem = myItem;
    }
}