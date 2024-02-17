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
    public bool IsHol() // Ȧ�ΰ���?
    {
        if(DoorH._CarDoor%2==1) // ���䰪�� Ȧ�̶��
        {
            return true; // ���䰪�� Ȧ
        }
        return false; // ���䰪�� ¦
    }

    public bool IsZzak() // ¦�ΰ���?
    {
        if (DoorH._CarDoor % 2 == 0) // ���䰪�� ¦�̶��
        {
            return true; // ���䰪�� ¦
        }
        return false; // ���䰪�� Ȧ
    }
    //  �ϳ� �̸� �����
    public void IsOpen()
    {
        DoorH.DoorItemOpen();
        return;
    }

    // ���� Ȯ��
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
            DoorUIHandler.DoorUIH.StartPlayerText("�� �̻� ������ �� ����...");
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
                    DoorUIHandler.DoorUIH.StartRabbitText("�� ������ ��.");
                    DoorUIHandler.DoorUIH.Maybe(list);
                    return;
                }
        }
        DoorUIHandler.DoorUIH.StartRabbitText("����..");
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
        DoorUIHandler.DoorUIH.StartRabbitText("���� ����.");
        DoorUIHandler.DoorUIH.Wrong(list);
    }

    public void ShowDoor()
    {
        DoorUIHandler.DoorUIH.StartRabbitText("�� �� �� ����?");

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
