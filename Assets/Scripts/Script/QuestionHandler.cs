using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionHandler : MonoBehaviour
{
    private int num;
    public DoorHandler _doorhandler;
    private void Start()
    {
        _doorhandler = DoorHandler.DoorH;
    }
    public bool IsHol() // Ȧ�ΰ���?
    {
        if(_doorhandler._CarDoor%2==1) // ���䰪�� Ȧ�̶��
        {
            return true; // ���䰪�� Ȧ
        }
        return false; // ���䰪�� ¦
    }

    public bool IsZzak() // ¦�ΰ���?
    {
        if (_doorhandler._CarDoor % 2 == 0) // ���䰪�� ¦�̶��
        {
            return true; // ���䰪�� ¦
        }
        return false; // ���䰪�� Ȧ
    }
    //  �ϳ� �̸� �����
    public void IsOpen()
    {
        _doorhandler.DoorItemOpen();
        return;
    }

    // ���� Ȯ��
    public void IsCheckDoor(int type = 0)
    {
        _doorhandler.DoorChecking(type);
        return;
    }
    



}
