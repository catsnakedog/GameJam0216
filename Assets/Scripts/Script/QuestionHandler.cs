using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        if(type == 0 || type == 1 || type == 2 || type == 3)
            TypeCheck(type);
        if(type == 5)
            HolZzakCheck();
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
                    DoorUIHandler.DoorUIH.Maybe(list);
                    return;
                }
        }
        DoorUIHandler.DoorUIH.Wrong(list);
    }

    public void HolZzakCheck()
    {
        List<int> list = new();

        if (DoorHandler.DoorH._CarDoor % 2 == 1)
        {    
            for(int i = 0; i < DoorHandler.DoorH.DoorTypes.Count; i++)
            {
                if (i % 2 == 1)
                    list.Add(i);
            }
        }
        else
        {
            for (int i = 0; i < DoorHandler.DoorH.DoorTypes.Count; i++)
            {
                if (i % 2 == 0)
                    list.Add(i);
            }
        }

        DoorUIHandler.DoorUIH.Wrong(list);
    }
}
