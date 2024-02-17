using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionHandler : MonoBehaviour
{
    private int num;
    public DoorHandler DoorH;
    public static QuestionHandler QuestionH;
    private void Start()
    {
        DoorH = DoorHandler.DoorH;
        QuestionH = this;
    }
    public bool IsHol() // 홀인가요?
    {
        if(DoorH._CarDoor%2==1) // 정답값이 홀이라면
        {
            return true; // 정답값이 홀
        }
        return false; // 정답값이 짝
    }

    public bool IsZzak() // 짝인가요?
    {
        if (DoorH._CarDoor % 2 == 0) // 정답값이 짝이라면
        {
            return true; // 정답값이 짝
        }
        return false; // 정답값이 홀
    }
    //  하나 미리 열어보기
    public void IsOpen()
    {
        DoorH.DoorItemOpen();
        return;
    }

    // 종류 확인
    public void IsCheckDoor(int type = 0)
    {
        DoorH.DoorChecking(type);
        return;
    }

    public void UseCard(int type)
    {
        Data.GameData.InGameData.CardUseCount++;
        if(type == 0 || type == 1 || type == 2 || type == 3)
            TypeCheck(type);
        if(type == 4)
            HolZzakCheck();
        if (type == 5)
            ShowDoor();

        if(Data.GameData.InGameData.CardUseCount >= 2)
            DoorUIHandler.DoorUIH.StartPlayerText("더 이상 질문할 수 없어...");
    }

    public void TypeCheck(int type)
    {
        List<int> list = new();

        for (int i = 0; i < DoorHandler.DoorH.DoorTypes.Count; i++)
        {
            if (DoorHandler.DoorH.DoorTypes[i] == type)
                list.Add(i);
        }

        for (int i = 0; i < DoorHandler.DoorH.DoorTypes.Count; i++)
        {
            if (DoorHandler.DoorH.DoorTypes[i] == type)
                if (DoorHandler.DoorH.DoorTypes[i] == DoorHandler.DoorH.DoorTypes[DoorHandler.DoorH._CarDoor])
                {
                    DoorUIHandler.DoorUIH.StartRabbitText("오 예리한 걸.");
                    DoorUIHandler.DoorUIH.Maybe(list);
                    return;
                }
        }
        DoorUIHandler.DoorUIH.StartRabbitText("저런..");
        DoorUIHandler.DoorUIH.Wrong(list);
    }

    public void HolZzakCheck()
    {
        List<int> list = new();

        if (DoorHandler.DoorH._CarDoor % 2 == 1)
        {    
            for(int i = 0; i < DoorHandler.DoorH.DoorTypes.Count; i++)
            {
                if (i % 2 == 0)
                    list.Add(i);
            }
        }
        else
        {
            for (int i = 0; i < DoorHandler.DoorH.DoorTypes.Count; i++)
            {
                if (i % 2 == 1)
                    list.Add(i);
            }
        }
        DoorUIHandler.DoorUIH.StartRabbitText("운이 좋네.");
        DoorUIHandler.DoorUIH.Wrong(list);
    }

    public void ShowDoor()
    {
        DoorUIHandler.DoorUIH.StartRabbitText("잘 고를 수 있지?");

        if (Data.GameData.InGameData.SelectDoorIdx == DoorHandler.DoorH._CarDoor)
            StartCoroutine(DoorUIHandler.DoorUIH.YesNoEffect(Managers.Resource.Load<Sprite>("DoorYes")));
        else
        {
            DoorUIHandler.DoorUIH.DoorIcons[Data.GameData.InGameData.SelectDoorIdx].GetComponent<Image>().sprite = Managers.Resource.Load<Sprite>("No");
            StartCoroutine(DoorUIHandler.DoorUIH.YesNoEffect(Managers.Resource.Load<Sprite>("DoorNo")));
        }
        CardHandler.instance.isUseCard = true;
    }
}
