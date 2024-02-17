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
        DoorUIHandler.DoorUIH.StartRabbitText("�Ǹ��̾�.");
        CardHandler.instance.cardSpawnPoint.transform.position = new Vector2(0, 2);
        GetRandomCard();
        gameObject.GetComponent<SpriteRenderer>().sprite = Managers.Resource.Load<Sprite>("DoorFail");
        CardHandler.instance.isUseCard = false;
        DoorUIHandler.DoorUIH.IsRun = true;
        StartCoroutine(DoorUIHandler.DoorUIH.GameEndEffect());
    }

    public void CarDoorOpen()
    {
        Data.GameData.InGameData.SuccessStack++;
        CardHandler.instance.cardSpawnPoint.transform.position = new Vector2(0, 2);
        GetRandomCard();
        GetRandomCard();
        switch (Data.GameData.InGameData.SuccessStack)
        {
            case 1:
                DoorUIHandler.DoorUIH.StartRabbitText("�Ӹ��� ����� �ƴϿ���.");
                break;
            case 2:
                DoorUIHandler.DoorUIH.StartRabbitText("�� �߾�.");
                break;
            case 3:
                DoorUIHandler.DoorUIH.StartRabbitText("�����.");
                break;
            case 4:
                DoorUIHandler.DoorUIH.StartRabbitText("���� �� ������?");
                break;
            case 5:
                DoorUIHandler.DoorUIH.StartRabbitText("�ʶ�� �س� �� �˾Ҿ�.");
                break;
            case 6:
                DoorUIHandler.DoorUIH.StartRabbitText("���� �Ǹ���.");
                break;
        }
        gameObject.GetComponent<SpriteRenderer>().sprite = Managers.Resource.Load<Sprite>("DoorClear");
        CardHandler.instance.isUseCard = false;
        DoorUIHandler.DoorUIH.IsRun = true;
        StartCoroutine(DoorUIHandler.DoorUIH.GameEndEffect());
    }

    void GetRandomCard()
    {
        int num = Random.Range(1, 101);
        if (num <= 21)
            CardHandler.instance.FindAndAddCard(0);
        else if (num <= 42)
            CardHandler.instance.FindAndAddCard(1);
        else if (num <= 63)
            CardHandler.instance.FindAndAddCard(2);
        else if (num <= 84)
            CardHandler.instance.FindAndAddCard(3);
        else if (num <= 96)
            CardHandler.instance.FindAndAddCard(4);
        else
            CardHandler.instance.FindAndAddCard(5);
    }

    public void OpenDoor()
    {
        DoorUIHandler.DoorUIH.DoorIcons[Data.GameData.InGameData.SelectDoorIdx].GetComponent<Image>().sprite = Managers.Resource.Load<Sprite>("No");
        DoorOpended = true;
        StartCoroutine(DoorEffect(Managers.Resource.Load<Sprite>("DoorFail")));
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

        Managers.Sound.Play("Door");

        if (!_doorhandler.FirstClicked) // ù��° Ŭ���̸�, 
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
