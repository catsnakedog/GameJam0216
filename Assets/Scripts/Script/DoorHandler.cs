using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorHandler : MonoBehaviour
{
    public static DoorHandler DoorH;
    public GameObject door;
    public List<GameObject> Prefabs;
    private float positioned = 0;
    private int _randomN;
    private int _N;
    private int ThisDoorNumber;

    public bool FirstClicked = false;
    
    List<GameObject> Doors = new List<GameObject>();

    private void Awake()
    {
        DoorH = this;
        Generate();
    }
    void Generate()
    {
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
        _randomN = Random.Range(0, Doors.Count); // �� ����Ʈ�� ������ �ϳ� ����.
        Doors[_randomN].transform.GetChild(0).GetComponent<Door>().ThisDoor = 1; // �ڽ� ���� �ڵ�� 1
    }

    public void MohntiholAction()
    {
        while (true)
        {
            _N = Random.Range(0, Doors.Count);
            if (_randomN != _N && Doors[_N].transform.GetChild(0).GetComponent<Door>().ThisDoor == 0&&_N!=ThisDoorNumber) // ������ �ڵ����� �ƴϰ�, ������ ���ҿ��� �ϰ�, ������ ���� �� ���� �ƴҶ�
            {
                FirstClicked = true;
                Doors[_N].transform.GetChild(0).GetComponent<Door>().OnMouseDown(); // Ŭ�� ó��, �̶� ���ӿ����ȵǰ� �۵��ؾ���.
                return;
            }
        }
    }

    // Stage1 �϶�
    void GetStage1()
    {
        
    }

}
