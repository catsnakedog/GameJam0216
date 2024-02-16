using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool DoorOpended=false;
    public int ThisDoor = 0;  // 0 염소, 1 자동차
    public DoorHandler _doorhandler;
    public int Doortype; // 0, 1 ,2 , 3    블루, 그린, 레드, 옐로우.

   
    // Start is called before the first frame update
    void Start()
    {
        _doorhandler = DoorHandler.DoorH;
    }

    // Update is called once per frame  
    void Update()
    {

    }

    public void GoatDoorOpen()
    {
        Debug.Log("염소");
        this.GetComponent<SpriteRenderer>().color = new Color(0, 0, 255, 255);
    }

    public void CarDoorOpen()
    {
        Debug.Log("자동차");
        this.GetComponent<SpriteRenderer>().color = new Color(255, 0, 0, 255);
    }

    public void OnMouseDown()
    {
        if(!_doorhandler.FirstClicked) // 첫번째 클릭이면, 
        {
            _doorhandler.MohntiholAction();
            return;
        }

        if(_doorhandler.IsOpenbool==true)  // 미리열어보기 아이템 사용시 진입가능 , 첫번째 클릭전 아이템 사용 불가 해야함.
        {
            if (ThisDoor == 0)
            {
                GoatDoorOpen();
            }
            else if (ThisDoor == 1)
            {
                CarDoorOpen();
            }
            _doorhandler.IsOpenbool = false;
            DoorOpended = true; // 열어둔걸로
            return;
        }

        DoorOpended = true;// 이 문은 열렸다
        // 어떤 문을 열었는지 체크
        if (ThisDoor == 0)
        {
            GoatDoorOpen();
        }
        else if(ThisDoor == 1)
        {
            CarDoorOpen();
        }

        if(_doorhandler.FirstClicked==true)
        {
            Debug.Log("게임오버");
        }
    }
 
}
