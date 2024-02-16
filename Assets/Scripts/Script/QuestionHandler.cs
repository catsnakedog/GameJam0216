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
    public bool IsHol() // 홀인가요?
    {
        if(_doorhandler._CarDoor%2==1) // 정답값이 홀이라면
        {
            return true; // 정답값이 홀
        }
        return false; // 정답값이 짝
    }

    public bool IsZzak() // 짝인가요?
    {
        if (_doorhandler._CarDoor % 2 == 0) // 정답값이 짝이라면
        {
            return true; // 정답값이 짝
        }
        return false; // 정답값이 홀
    }
    //  하나 미리 열어보기
    public void IsOpen()
    {
        _doorhandler.DoorItemOpen();
        return;
    }

    // 종류 확인
    public void IsCheckDoor(int type = 0)
    {
        _doorhandler.DoorChecking(type);
        return;
    }
    



}
