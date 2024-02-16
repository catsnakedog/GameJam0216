using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorUIHandler : MonoBehaviour
{
    public static DoorUIHandler DoorUIH;
    public GameObject Door;
    public GameObject DoorBox;
    private GameObject[] _doors;
    private int beforeSelectDoorIdx;

    void Awake()
    {
        DoorUIH = this;
    }

    private void Start()
    {
        DOTween.Init();
        GenerateDoor();
    }

    public void GenerateDoor()
    {
        DoorHandler.DoorH.DoorTypes = new List<int> { 1, 1, 1, 1 };

        _doors = new GameObject[DoorHandler.DoorH.DoorTypes.Count];

        for(int i = 0; i < DoorHandler.DoorH.DoorTypes.Count; i++)
        {
            _doors[i] = Instantiate(Door, new Vector2(3f, 1.2f), Util.QI, DoorBox.transform);
            _doors[i].GetComponent<SpriteRenderer>().sprite = Managers.Resource.Load<Sprite>($"Door{DoorHandler.DoorH.DoorTypes[i]}");
        }

        Data.GameData.InGameData.SelectDoorIdx = 0;
        beforeSelectDoorIdx = Data.GameData.InGameData.SelectDoorIdx;

        SetDoorsPos();
    }

    public void ChangeSelectDoor()
    {
        if(beforeSelectDoorIdx < Data.GameData.InGameData.SelectDoorIdx)
        {
            beforeSelectDoorIdx++;
            SetDoorsPos();
        }
        else if (beforeSelectDoorIdx > Data.GameData.InGameData.SelectDoorIdx)
        {
            beforeSelectDoorIdx--;
            SetDoorsPos();
        }
    }

    void SetDoorsPos()
    {
        for (int i = 0; i < _doors.Length; i++)
        {
            float x = (0 - 5.15f * (beforeSelectDoorIdx - i));
            float y = 1.2f;

            float scale = 0.8f;
            if (i == beforeSelectDoorIdx)
                scale = 1f;

            float changeTime = 0.5f;
            _doors[i].transform.DOMove(new Vector3(x, y, 0), changeTime);
            _doors[i].transform.DOScale(scale, changeTime).OnComplete(ChangeSelectDoor);
        }
    }
}
