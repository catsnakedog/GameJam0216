using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Door : MonoBehaviour
{
    public bool DoorOpended=false;
    public int ThisDoor = 0;  // 0 염소, 1 자동차
    public DoorHandler _doorhandler;
    public int Doortype; // 0, 1 ,2 , 3    블루, 그린, 레드, 옐로우.
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
        DoorUIHandler.DoorUIH.StartRabbitText("실망이야.");
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
                DoorUIHandler.DoorUIH.StartRabbitText("머리가 장식이 아니였군.");
                break;
            case 2:
                DoorUIHandler.DoorUIH.StartRabbitText("잘 했어.");
                break;
            case 3:
                DoorUIHandler.DoorUIH.StartRabbitText("대단해.");
                break;
            case 4:
                DoorUIHandler.DoorUIH.StartRabbitText("고작 이 정도야?");
                break;
            case 5:
                DoorUIHandler.DoorUIH.StartRabbitText("너라면 해낼 줄 알았어.");
                break;
            case 6:
                DoorUIHandler.DoorUIH.StartRabbitText("아주 훌륭해.");
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

        if (!_doorhandler.FirstClicked) // 첫번째 클릭이면, 
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
    }
}
