using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Door : MonoBehaviour
{
    public bool DoorOpended=false;
    public int ThisDoor = 0;  // 0 ����, 1 �ڵ���
    public DoorHandler _doorhandler;
    public int Doortype; // 0, 1 ,2 , 3    ���, �׸�, ����, ���ο�.
    public bool isActive;

   
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

    public void OpenDoor()
    {
        DoorUIHandler.DoorUIH.DoorIcons[Data.GameData.InGameData.SelectDoorIdx].GetComponent<Image>().sprite = Managers.Resource.Load<Sprite>("No");
        DoorOpended = true;
        StartCoroutine(DoorEffect(Managers.Resource.Load<Sprite>("No")));
    }

    IEnumerator DoorEffect(Sprite sprite)
    {
        float fillAmount = 1f;
        SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer>();
        while (fillAmount > 0)
        {
            fillAmount -= Time.deltaTime * 1f;
            renderer.color = new Color(1f, 1f, 1f, fillAmount);
            yield return null;
        }
        yield return new WaitForSeconds(0.1f);
        renderer.sprite = sprite;
        while (fillAmount < 1)
        {
            fillAmount += Time.deltaTime * 1f;
            renderer.color = new Color(1f, 1f, 1f, fillAmount);
            yield return null;
        }
    }

    public void OnMouseDown()
    {
        if (DoorOpended)
            return;
        if (DoorUIHandler.DoorUIH.Doors[Data.GameData.InGameData.SelectDoorIdx] != gameObject)
            return;
        if (DoorUIHandler.DoorUIH.IsRun)
            return;

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
    }
}
