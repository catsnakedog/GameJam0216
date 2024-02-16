using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorHandler : MonoBehaviour
{
    public static DoorHandler DoorH;
    public int _CarDoor;
    private int _N;

    // �������� ���� ������ �����ϴµ� ����ϴ� �ڵ�
    private StageData _stagedata;
    public List<int> DoorTypes;



    public bool IsOpenbool = false;

    public bool FirstClicked = false;

    private void Start()
    {
        DoorH = this;
        FirstClicked = false;
        GetStageData();//�ӽ� �����
    }
    /*
    void Generate()
    {            // �� ���� 3, 4, 4 , 5 ,5 , 6 .  
                 // �� ���� 1:1:2:0  /  1:1:1:1 / 1:1:3 / 1:1:2  / 1:1:1:3 / 1:1:2:2
      
        for(int i =0; i<3; i++) // Ȱ��ȭ
        {
            Doors.Add(Instantiate(Prefabs[0], transform));
            Doors[i].SetActive(true);
            Doors[i].transform.position = new Vector3(-1.8f+positioned, 1, 0); // �ʱ� ��ġ ����
            positioned += 1.8f; //�̵���ų ĭ ũ��

            ThisDoorNumber = i; // �� ������ ���� ����
        }
        GetRandomCarDoor();
    }
    */


    public void MohntiholAction()
    {
        DoorUIHandler.DoorUIH.StartRabbitText("�ùٸ��� ���� �� �ϳ��� �����帮�ڽ��ϴ�.");
        while (true)
        {
            _N = Random.Range(0, DoorTypes.Count);
            if (_CarDoor != _N && _N!=Data.GameData.InGameData.SelectDoorIdx) // ������ �ڵ����� �ƴϰ�, ������ ���ҿ��� �ϰ�, ������ ���� �� ���� �ƴҶ�
            {
                FirstClicked = true;
                Data.GameData.InGameData.SelectDoorIdx = _N;
                StartCoroutine(DoorUIHandler.DoorUIH.OpenOneWrongDoor());
                break;
            }
        }
    }

    public void DoorItemOpen() // ���������� �� ����
    {
        IsOpenbool = true; // ���� ������ ���������� ����
        //������ Ŭ�� ǥ���� �ؽ�Ʈ �ʿ�
    }

    public bool DoorChecking(int type)
    {
        if (type == DoorTypes[_CarDoor])
        {
            return true;
        }
        else return false;
    }
    
    public void GetStageData()
    {
        _stagedata = Data.GameData.StageData;
        //���������� ���� ������ ����.
        // doornumber = _stagedata.StageInfo[stagenumber].Door0 + _stagedata.StageInfo[stagenumber].Door1 + _stagedata.StageInfo[stagenumber].Door2 + _stagedata.StageInfo[stagenumber].Door3;

        Data.GameData.InGameData.Stage = 5;

        // ������� ���缭 ����Ʈ�� �ִ´�.
        for(int i =0; i<_stagedata.StageInfo[Data.GameData.InGameData.Stage].Door0;i++)
        {
            DoorTypes.Add(0);
        }
        for (int i = 0; i < _stagedata.StageInfo[Data.GameData.InGameData.Stage].Door1; i++)
        {
            DoorTypes.Add(1);
        }
        for (int i = 0; i < _stagedata.StageInfo[Data.GameData.InGameData.Stage].Door2; i++)
        {
            DoorTypes.Add(2);
        }
        for (int i = 0; i < _stagedata.StageInfo[Data.GameData.InGameData.Stage].Door3; i++)
        {
            DoorTypes.Add(3);
        }
    

        // �������� ����
        DoorTypes = ShuffleList(DoorTypes); // DoorTypes[0]���� ù��°��   
        _CarDoor = Random.Range(0, DoorTypes.Count); // ���� _CarDoor��° ���� ����.

        DoorUIHandler.DoorUIH.GenerateDoor();
    }

    // ����Ʈ�� �����ִ� �Լ�
    private List<T> ShuffleList<T>(List<T> list)
    {
        int random1, random2;
        T temp;

        for (int i = 0; i < list.Count; ++i)
        {
            random1 = Random.Range(0, list.Count);
            random2 = Random.Range(0, list.Count);

            temp = list[random1];
            list[random1] = list[random2];
            list[random2] = temp;
        }

        return list;
    }

}
