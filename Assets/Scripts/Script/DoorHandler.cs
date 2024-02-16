using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorHandler : MonoBehaviour
{
    public static DoorHandler DoorH;
    public int _CarDoor;
    private int _N;

    // 스테이지 문의 순서를 결정하는데 사용하는 코드
    private StageData _stagedata;
    public List<int> DoorTypes;



    public bool IsOpenbool = false;

    public bool FirstClicked = false;

    private void Start()
    {
        DoorH = this;
        FirstClicked = false;
        GetStageData();//임시 디버깅
    }
    /*
    void Generate()
    {            // 문 개수 3, 4, 4 , 5 ,5 , 6 .  
                 // 문 종류 1:1:2:0  /  1:1:1:1 / 1:1:3 / 1:1:2  / 1:1:1:3 / 1:1:2:2
      
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
    */


    public void MohntiholAction()
    {
        DoorUIHandler.DoorUIH.StartRabbitText("올바르지 못한 문 하나를 보여드리겠습니다.");
        while (true)
        {
            _N = Random.Range(0, DoorTypes.Count);
            if (_CarDoor != _N && _N!=Data.GameData.InGameData.SelectDoorIdx) // 랜덤이 자동차가 아니고, 랜덤이 염소여야 하고, 랜덤이 내가 고른 문이 아닐때
            {
                FirstClicked = true;
                Data.GameData.InGameData.SelectDoorIdx = _N;
                StartCoroutine(DoorUIHandler.DoorUIH.OpenOneWrongDoor());
                break;
            }
        }
    }

    public void DoorItemOpen() // 아이템으로 문 열기
    {
        IsOpenbool = true; // 다음 오픈은 아이템으로 오픈
        //아이템 클릭 표시할 텍스트 필요
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
        //스테이지의 도어 갯수를 센다.
        // doornumber = _stagedata.StageInfo[stagenumber].Door0 + _stagedata.StageInfo[stagenumber].Door1 + _stagedata.StageInfo[stagenumber].Door2 + _stagedata.StageInfo[stagenumber].Door3;

        Data.GameData.InGameData.Stage = 5;

        // 도어수에 맞춰서 리스트에 넣는다.
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
    

        // 랜덤으로 섞기
        DoorTypes = ShuffleList(DoorTypes); // DoorTypes[0]부터 첫번째문   
        _CarDoor = Random.Range(0, DoorTypes.Count); // 문중 _CarDoor번째 문이 정답.

        DoorUIHandler.DoorUIH.GenerateDoor();
    }

    // 리스트를 섞어주는 함수
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
