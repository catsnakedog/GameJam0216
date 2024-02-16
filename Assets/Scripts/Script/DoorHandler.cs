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
        for(int i =0; i<3; i++) // 활성화
        {
            Doors.Add(Instantiate(Prefabs[0], transform));
            Doors[i].SetActive(true);
            Doors[i].transform.position = new Vector3(-1.8f+positioned, 1, 0); // 초기 위치 설정
            positioned += 1.8f; //이동시킬 칸 크기

            ThisDoorNumber = i; // 이 도어의 숫자 지정
        }
        GetRandomCarDoor();
    }

    void GetRandomCarDoor()
    {
        _randomN = Random.Range(0, Doors.Count); // 문 리스트의 개수중 하나 선택.
        Doors[_randomN].transform.GetChild(0).GetComponent<Door>().ThisDoor = 1; // 자식 도어 코드는 1
    }

    public void MohntiholAction()
    {
        while (true)
        {
            _N = Random.Range(0, Doors.Count);
            if (_randomN != _N && Doors[_N].transform.GetChild(0).GetComponent<Door>().ThisDoor == 0&&_N!=ThisDoorNumber) // 랜덤이 자동차가 아니고, 랜덤이 염소여야 하고, 랜덤이 내가 고른 문이 아닐때
            {
                FirstClicked = true;
                Doors[_N].transform.GetChild(0).GetComponent<Door>().OnMouseDown(); // 클릭 처리, 이때 게임오버안되게 작동해야함.
                return;
            }
        }
    }

    // Stage1 일때
    void GetStage1()
    {
        
    }

}
