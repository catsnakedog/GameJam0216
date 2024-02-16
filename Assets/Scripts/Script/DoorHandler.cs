using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorHandler : MonoBehaviour
{
    public static DoorHandler DoorH;
    public GameObject door;
    public List<GameObject> Prefabs;
    private float positioned = 0;
    public int _CarDoor;
    private int _N;
    private int ThisDoorNumber;

    // �������� ���� ������ �����ϴµ� ����ϴ� �ڵ�
    private int stagenumber = 0;
    private StageData _stagedata;
    private int doornumber = 0;
    public List<int> DoorTypes;



    public bool IsOpenbool = false;

    public bool FirstClicked = false;
    
    public List<GameObject> Doors = new List<GameObject>();

    private void Awake()
    {
        DoorH = this;
        Generate();
        // ���� ����GetStageData();//�ӽ� �����
    }
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

    void GetRandomCarDoor()
    {
        _CarDoor = Random.Range(0, Doors.Count); // �� ����Ʈ�� ������ �ϳ� ����.
        Doors[_CarDoor].transform.GetChild(0).GetComponent<Door>().ThisDoor = 1; // �ڽ� ���� �ڵ�� 1
    }

    public void MohntiholAction()
    {
        while (true)
        {
            _N = Random.Range(0, Doors.Count);
            if (_CarDoor != _N && Doors[_N].transform.GetChild(0).GetComponent<Door>().ThisDoor == 0&&_N!=ThisDoorNumber) // ������ �ڵ����� �ƴϰ�, ������ ���ҿ��� �ϰ�, ������ ���� �� ���� �ƴҶ�
            {
                FirstClicked = true;
                Doors[_N].transform.GetChild(0).GetComponent<Door>().OnMouseDown(); // Ŭ�� ó��, �̶� ���ӿ����ȵǰ� �۵��ؾ���.
                return;
            }
        }
    }

    public void DoorItemOpen() // ���������� �� ����
    {
        IsOpenbool = true; // ���� ������ ���������� ����
        //������ Ŭ�� ǥ���� �ؽ�Ʈ �ʿ�
    }

    public void DoorChecking(int type)
    {

    }
    
    public void GetStageData()
    {
        _stagedata = Managers.DataManager.GameData.StageData;
        //���������� ���� ������ ����.
        // doornumber = _stagedata.StageInfo[stagenumber].Door0 + _stagedata.StageInfo[stagenumber].Door1 + _stagedata.StageInfo[stagenumber].Door2 + _stagedata.StageInfo[stagenumber].Door3;
        

        // ������� ���缭 ����Ʈ�� �ִ´�.
        for(int i =0; i<_stagedata.StageInfo[stagenumber].Door0;i++)
        {
            DoorTypes.Add(0);
        }
        for (int i = 0; i < _stagedata.StageInfo[stagenumber].Door1; i++)
        {
            DoorTypes.Add(1);
        }
        for (int i = 0; i < _stagedata.StageInfo[stagenumber].Door2; i++)
        {
            DoorTypes.Add(2);
        }
        for (int i = 0; i < _stagedata.StageInfo[stagenumber].Door3; i++)
        {
            DoorTypes.Add(3);
        }

        // �������� ����
        DoorTypes = ShuffleList(DoorTypes);

        Debug.Log(DoorTypes);
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
