using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool DoorOpended=false;
    public int ThisDoor = 0;  // 0 ����, 1 �ڵ���
    public DoorHandler _doorhandler;
    public int Doortype; // 0, 1 ,2 , 3    ���, �׸�, ����, ���ο�.

   
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
        Debug.Log("����");
        this.GetComponent<SpriteRenderer>().color = new Color(0, 0, 255, 255);
    }

    public void CarDoorOpen()
    {
        Debug.Log("�ڵ���");
        this.GetComponent<SpriteRenderer>().color = new Color(255, 0, 0, 255);
    }

    public void OnMouseDown()
    {
        if(!_doorhandler.FirstClicked) // ù��° Ŭ���̸�, 
        {
            _doorhandler.MohntiholAction();
            return;
        }

        if(_doorhandler.IsOpenbool==true)  // �̸������ ������ ���� ���԰��� , ù��° Ŭ���� ������ ��� �Ұ� �ؾ���.
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
            DoorOpended = true; // ����аɷ�
            return;
        }

        DoorOpended = true;// �� ���� ���ȴ�
        // � ���� �������� üũ
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
            Debug.Log("���ӿ���");
        }
    }
 
}
