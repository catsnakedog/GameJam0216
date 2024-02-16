using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StageData
{
    public List<StageInfo> StageInfo;

    public StageData()
    {
        StageInfo = new();
    }
    public StageData(List<StageInfo> stageInfo)
    {
        StageInfo = stageInfo;
    }   
}

[System.Serializable]
public class StageInfo
{
    public int Stage;
    public int Door0;
    public int Door1;
    public int Door2;
    public int Door3;

    public StageInfo()
    {
        Stage = 0;
        Door0 = 0;
        Door1 = 0;
        Door2 = 0;
        Door3 = 0;
    }
    public StageInfo(int stage, int door0,int door1,int door2,int door3)
    {
        Stage = stage;
        Door0 = door0;
        Door1 = door1;
        Door2 = door2;
        Door3 = door3;
    }
}
