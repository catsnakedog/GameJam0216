using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StageData
{
    int DoorCount;

    public StageData()
    {
        DoorCount = 0;
    }
    public StageData(int doorCount)
    {
        DoorCount = doorCount;
    }
}
