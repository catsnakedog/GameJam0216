using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DoorUIHandler : MonoBehaviour
{
    public static DoorUIHandler DoorUIH;
    public GameObject Door;
    public GameObject DoorBox;
    public GameObject Icons;
    public GameObject Icon;
    public Button LeftDoor;
    public Button RightDoor;
    public GameObject[] Doors;
    public GameObject[] DoorIcons;
    private int beforeSelectDoorIdx;
    public bool IsRun;
    Coroutine coroutine;
    public TMP_Text PlayerText;
    public TMP_Text RabbitText;
    Coroutine rabbitC;
    Coroutine playerC;
    public GameObject YesNo;
    public GameObject SelectBox;
    public GameObject FadeBox;

    void Awake()
    {
        DoorUIH = this;
    }

    private void Start()
    {
        LeftDoor.onClick.AddListener(MoveToLeftDoor);
        RightDoor.onClick.AddListener(MoveToRightDoor);
        SelectBox.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(SelectYes);
        SelectBox.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(SelectNo);
        IsRun = false;

        StartRabbitText("어떤 문을 선택할래?");
    }

    public void SelectYes()
    {
        QuestionHandler.QuestionH.UseCard(CardHandler.instance.selectCard.Item.Type);
        SelectBox.SetActive(false);
    }

    public void SelectNo()
    {
        CardHandler.instance.SpawnSelectCard();
        SelectBox.SetActive(false);
        CardHandler.instance.isUseCard = true;
    }

    public void StartPlayerText(string text)
    {
        text = text.Replace("\\n", "\n");
        if (playerC != null)
            StopCoroutine(playerC);
        playerC = StartCoroutine(SetPlayerText(text));
    }
    public void StartRabbitText(string text)
    {
        text = text.Replace("\\n", "\n");
        if (rabbitC != null)
            StopCoroutine(rabbitC);
        rabbitC = StartCoroutine(SetRabbitText(text));
    }

    public IEnumerator SetPlayerText(string text)
    {
        StringBuilder sb = new();
        foreach(char s in text)
        {
            Managers.Sound.Play("Chat");
            sb.Append(s);
            PlayerText.text = sb.ToString();
            yield return new WaitForSeconds(0.045f);
        }
    }

    public IEnumerator SetRabbitText(string text)
    {
        StringBuilder sb = new();
        foreach (char s in text)
        {
            Managers.Sound.Play("Chat");
            sb.Append(s);
            RabbitText.text = sb.ToString();
            yield return new WaitForSeconds(0.125f);
        }
    }

    void MoveToLeftDoor()
    {
        if (IsRun)
            return;
        if (Data.GameData.InGameData.SelectDoorIdx == 0)
            return;
        Data.GameData.InGameData.SelectDoorIdx--;
        ChangeSelectDoor();
    }

    void MoveToRightDoor()
    {
        if (IsRun)
            return;
        if (Data.GameData.InGameData.SelectDoorIdx >= DoorHandler.DoorH.DoorTypes.Count - 1)
            return;
        Data.GameData.InGameData.SelectDoorIdx++;
        ChangeSelectDoor();
    }

    public void GenerateDoor()
    {
        float[] iconPos = new float[0];

        switch(DoorHandler.DoorH.DoorTypes.Count)
        {
            case 3:
                iconPos = new float[3] { -130, 0, 130 };
                break;
            case 4:
                iconPos = new float[4] { -195, -65, 65, 195 };
                break;
            case 5:
                iconPos = new float[5] { -260, -130, 0, 130, 260 };
                break;
            case 6:
                iconPos = new float[6] { -325, -195, -65, 65, 195, 325 };
                break;
        }

        Doors = new GameObject[DoorHandler.DoorH.DoorTypes.Count];
        DoorIcons = new GameObject[DoorHandler.DoorH.DoorTypes.Count];

        for (int i = 0; i < DoorHandler.DoorH.DoorTypes.Count; i++)
        {
            GameObject icon = Instantiate(Icon, Icons.transform, false);
            icon.transform.localPosition = new Vector2(iconPos[i], 0);
            icon.GetComponent<DoorIcon>().IconIdx = i;
            icon.GetComponent<DoorIcon>().SelectUI();
            icon.GetComponent<Image>().sprite = Managers.Resource.Load<Sprite>($"icon{i+1}");
            DoorIcons[i] = icon;
            Doors[i] = Instantiate(Door, new Vector2(15f, 1.2f), Util.QI, DoorBox.transform);
            Doors[i].GetComponent<SpriteRenderer>().sprite = Managers.Resource.Load<Sprite>($"Door{DoorHandler.DoorH.DoorTypes[i]}");
            if (i == DoorHandler.DoorH._CarDoor)
                Doors[i].GetComponent<Door>().ThisDoor = 1;
        }

        Data.GameData.InGameData.SelectDoorIdx = 0;
        Data.GameData.InGameData.CardUseCount = 0;
        beforeSelectDoorIdx = Data.GameData.InGameData.SelectDoorIdx;
        if(coroutine != null)
            StopCoroutine(coroutine);
        coroutine = StartCoroutine(SetDoorsPos());
    }

    public void ChangeSelectDoor()
    {
        IsRun = true;
        if(beforeSelectDoorIdx < Data.GameData.InGameData.SelectDoorIdx)
        {
            if (beforeSelectDoorIdx >= DoorHandler.DoorH.DoorTypes.Count - 1)
            {
                beforeSelectDoorIdx = DoorHandler.DoorH.DoorTypes.Count - 1;
                return;
            }
            beforeSelectDoorIdx++;
            if (coroutine != null)
                StopCoroutine(coroutine);
            coroutine = StartCoroutine(SetDoorsPos());
        }
        else if (beforeSelectDoorIdx > Data.GameData.InGameData.SelectDoorIdx)
        {
            if (beforeSelectDoorIdx <= 0)
            {
                beforeSelectDoorIdx = 0;
                return;
            }
            beforeSelectDoorIdx--;
            if (coroutine != null)
                StopCoroutine(coroutine);
            coroutine = StartCoroutine(SetDoorsPos());
        }
        else
        {
            IsRun = false;
        }
    }

    IEnumerator SetDoorsPos()
    {
        Managers.Sound.Play("DoorMove");
        IsRun = true;
        for (int i = 0; i < Doors.Length; i++)
        {
            float x = (0 - 5.15f * (beforeSelectDoorIdx - i));
            float y = 1.2f;

            float scale = 0.8f;
            if (i == beforeSelectDoorIdx)
                scale = 1f;

            Doors[i].transform.DOKill();

            float changeTime = 0.3f;
            var tween = Doors[i].transform.DOMove(new Vector3(x, y, 0), changeTime);
            Doors[i].transform.DOScale(scale, changeTime);
            if(i == Doors.Length - 1)
            {
                yield return tween.WaitForCompletion();
                if (beforeSelectDoorIdx != Data.GameData.InGameData.SelectDoorIdx)
                    ChangeSelectDoor();
                else
                {
                    IsRun = false;
                }
            }
        }
    }

    public void DoorClick(int num)
    {
        Doors[num].GetComponent<Door>().OnMouseDown();
    }

    public IEnumerator OpenOneWrongDoor()
    {
        yield return new WaitForSeconds(0.5f);
        IsRun = true;
        ChangeSelectDoor();
        yield return new WaitUntil(() => !IsRun);
        Doors[Data.GameData.InGameData.SelectDoorIdx].GetComponent<Door>().OpenDoor();
    }

    public IEnumerator YesNoEffect(Sprite sprite)
    {
        SpriteRenderer renderer = YesNo.GetComponent<SpriteRenderer>();
        renderer.color = new Color(1f, 1f, 1f, 1f);
        renderer.sprite = sprite;
        YesNo.SetActive(true);
        float fillAmount = 1f;

        while(fillAmount > 0f)
        {
            fillAmount -= Time.deltaTime * 2f;
            renderer.color = new Color(1f, 1f, 1f, fillAmount);
            yield return null;
        }
        YesNo.SetActive(false);
    }


    public void Wrong(List<int> list)
    {
        StartCoroutine(WrongAll(list));
    }
    public IEnumerator WrongAll(List<int> list)
    {
        foreach(int i in list)
        {
            Debug.Log(i);
            IsRun = true;
            Data.GameData.InGameData.SelectDoorIdx = i;
            DoorUIHandler.DoorUIH.ChangeSelectDoor();
            yield return new WaitUntil(() => !IsRun);
            yield return StartCoroutine(YesNoEffect(Managers.Resource.Load<Sprite>("DoorNo")));
            DoorUIHandler.DoorUIH.DoorIcons[Data.GameData.InGameData.SelectDoorIdx].GetComponent<Image>().sprite = Managers.Resource.Load<Sprite>("No");
        }
        CardHandler.instance.isUseCard = true;
    }

    public void Maybe(List<int> list)
    {
        StartCoroutine(MaybeAll(list));
    }
    public IEnumerator MaybeAll(List<int> list)
    {
        foreach (int i in list)
        {
            Debug.Log(i);
            IsRun = true;
            Data.GameData.InGameData.SelectDoorIdx = i;
            DoorUIHandler.DoorUIH.ChangeSelectDoor();
            yield return new WaitUntil(() => !IsRun);
            yield return StartCoroutine(YesNoEffect(Managers.Resource.Load<Sprite>("DoorTri")));
        }
        CardHandler.instance.isUseCard = true;
    }

    public IEnumerator GameEndEffect()
    {
        Data.GameData.InGameData.Stage++;
        for (int i = 0; i < Doors.Length; i++)
        {
            if (i != Data.GameData.InGameData.SelectDoorIdx)
            {
                StartCoroutine(FadeOutRemove(Doors[i]));
                yield return new WaitForSeconds(0.4f);
                DoorIcons[Data.GameData.InGameData.SelectDoorIdx].GetComponent<Image>().sprite = Managers.Resource.Load<Sprite>("No");
            }
        }

        yield return new WaitForSeconds(0.5f);
        Save();

        UI_SceneManager.Instance.ChangeScene("Story");
    }

    IEnumerator FadeInAndLoadScene()
    {
        float fillAmount = 0;
        Image image = FadeBox.GetComponent<Image>();
        image.color = new Color(0f, 0f, 0f, 0f);
        FadeBox.SetActive(true);
        while (fillAmount < 1)
        {
            fillAmount += Time.deltaTime * 1;
            image.color = new Color(0f, 0f, 0f, fillAmount);
            yield return null;
        }
        SceneManager.LoadScene("Story");
    }

    void Save()
    {
        Data.GameData.SaveData.Stage = Data.GameData.InGameData.Stage;
        Data.GameData.SaveData.SuccessStack = Data.GameData.InGameData.SuccessStack;
        Data.GameData.SaveData.MyItem = Data.GameData.InGameData.MyItem;
        Managers.DataManager.Save(Data.GameData.SaveData);
    }

    IEnumerator FadeOutRemove(GameObject obj)
    {
        float fillAmount = 1;
        SpriteRenderer renderer = obj.GetComponent<SpriteRenderer>();
        while (fillAmount > 0)
        {
            fillAmount -= Time.deltaTime * 1.5f;
            renderer.color = new Color(1f, 1f, 1f, fillAmount);
            yield return null;
        }
        Destroy(obj);
    }
}
