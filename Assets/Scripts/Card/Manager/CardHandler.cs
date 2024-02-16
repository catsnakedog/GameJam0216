using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardHandler : MonoBehaviour
{
    public static CardHandler instance;
    void Awake() => instance = this;

    [SerializeField] ItemSO itemSO; // 카드 정보
    [SerializeField] GameObject cardPrefab; // 카드 원본
    [SerializeField] List<Card> myCard; // 스폰 된 카드들
    [SerializeField] GameObject UseCardCanvas;

    [SerializeField] Transform cardSpawnPoint; // 스폰 위치
    [SerializeField] Transform cardLeft; // 왼쪽 끝 기준
    [SerializeField] Transform cardRight; // 오른쪽 끝 기준

    bool isDrag;
    Vector3 mousePos;
    PRS prs;
    Card selectCard;
    [SerializeField] public bool isUseCard;

    private void Start()
    {
        myCard = new List<Card>();
        prs = new PRS(new Vector3(), Util.QI, Vector3.zero);
        DOTween.SetTweensCapacity(2000, 100);

        FindAndAddCard(0);
        FindAndAddCard(1);
        FindAndAddCard(2);
        FindAndAddCard(3);
        FindAndAddCard(4);
        FindAndAddCard(5);
    }

    public void GameReset()
    {
        DOTween.KillAll();
        DOTween.Clear();
        StopAllCoroutines();

        foreach (Card card in myCard)
        {
            Destroy(card.gameObject);
        }

        myCard.Clear();

        selectCard = null;
        isDrag = false;
        isUseCard = false;
    }

    public void SpawnSelectCard(string name)
    {
        Item selectCard;
        foreach(Item item in itemSO.Items)
        {
            if(item.Name == name)
            {
                selectCard = item;
                break;
            }
        }
    }

    public void FindAndAddCard(int type)
    {
        var cardObject = Instantiate(cardPrefab, cardSpawnPoint.position, Util.QI);
        var card = cardObject.GetComponent<Card>();

        Item targetItem = new Item();

        foreach(Item item in itemSO.Items)
        {
            if(item.Type == type)
            {
                targetItem = item;
                break;
            }
        }

        Data.GameData.InGameData.MyItem.Add(targetItem);
        card.Setup(targetItem);
        myCard.Add(card);

        SetOriginOrder();
        CardAlignment();
    }

    public void DeleteCard()
    {
        Data.GameData.InGameData.MyItem.Remove(selectCard.Item);
        myCard.Remove(selectCard);
        Destroy(selectCard.gameObject);

        SetOriginOrder();
        CardAlignment();
    }


    void SetOriginOrder()
    {
        int count = myCard.Count;
        for (int i = 0; i < count; i++)
        {
            var targetCard = myCard[i];
            targetCard.GetComponent<Order>().SetOriginOrder(i);
        }
    }

    void CardAlignment()
    {
        //DOTween.KillAll();
        //DOTween.Clear();

        List<PRS> originCardPRS;

        originCardPRS = RoundAlignment(cardLeft, cardRight, myCard.Count, 0.5f, Vector3.one);

        for (int i = 0; i < myCard.Count; i++)
        {
            var targetCard = myCard[i];

            targetCard.OriginPRS = originCardPRS[i];
            targetCard.MoveTransform(targetCard.OriginPRS, true, 0.7f);
        }
    }

    void TargetCardAlignment(Card card)
    {
        List<PRS> originCardPRS;

        originCardPRS = TargetRoundAlignment(cardLeft, cardRight, myCard.Count, 0.5f, Vector3.one, card);

        for (int i = 0; i < myCard.Count; i++)
        {
            var targetCard = myCard[i];

            targetCard.OriginPRS = originCardPRS[i];
            if (targetCard == card)
                targetCard.OriginPRS.pos += new Vector3(0, 0.7f);
            targetCard.MoveTransform(targetCard.OriginPRS, true, 0.7f);
        }
    }

    List<PRS> RoundAlignment(Transform leftTr, Transform rightTr, int objCount, float height, Vector3 scale)
    {
        float[] objLerps = new float[objCount];
        List<PRS> results = new List<PRS>(objCount);

        switch (objCount)
        {
            case 1: objLerps = new float[] { 0.5f }; break;
            case 2: objLerps = new float[] { 0.27f, 0.73f }; break;
            case 3: objLerps = new float[] { 0.1f, 0.5f, 0.9f }; break;
            default:
                float interval = 1f / (objCount - 1);
                for (int i = 0; i < objCount; i++)
                    objLerps[i] = interval * i;
                break;
        }

        for (int i = 0; i < objCount; i++)
        {
            var targetPos = Vector3.Lerp(leftTr.position, rightTr.position, objLerps[i]);
            var targetRot = Util.QI;
            if (objCount >= 4)
            {
                float curve = Mathf.Sqrt(Mathf.Abs(Mathf.Pow(height, 2) - Mathf.Pow(objLerps[i] - 0.5f, 2)));
                curve = height >= 0 ? curve : -curve;
                targetPos.y += curve;
                targetPos.y -= 0.13f;
                targetPos.z -= 10 * i;

                if(targetPos.y < -11f)
                {
                    targetPos.y = -11f;
                }

                targetRot = Quaternion.Slerp(leftTr.rotation, rightTr.rotation, objLerps[i]);
            }

            results.Add(new PRS(targetPos, targetRot, scale));
        }

        return results;
    }

    List<PRS> TargetRoundAlignment(Transform leftTr, Transform rightTr, int objCount, float height, Vector3 scale, Card target)
    {
        float[] objLerps = new float[objCount];
        List<PRS> results = new List<PRS>(objCount);

        int targetIdx = myCard.IndexOf(target);
        float targetInterval = 0.35f;

        switch (objCount)
        {
            case 1: objLerps = new float[] { 0.5f }; break;
            case 2: objLerps = new float[] { 0.27f, 0.73f }; break;
            case 3: objLerps = new float[] { 0.1f, 0.5f, 0.9f }; break;
            default:
                if (targetIdx == 0)
                {
                    float interval = (1f-targetInterval) / (objCount - 2);
                    objLerps[0] = 0f;
                    objLerps[1] = targetInterval;
                    for (int i = 2; i < objCount; i++)
                        objLerps[i] = objLerps[i - 1] + interval;
                    break;
                }
                if (targetIdx == objCount - 1)
                {
                    float interval = (1f - targetInterval) / (objCount - 2);
                    for (int i = 0; i < targetIdx; i++)
                        objLerps[i] = i * interval;
                    objLerps[targetIdx] = objLerps[targetIdx - 1] + targetInterval;
                    break;
                }
                else
                {
                    float interval = (1f - targetInterval) / (objCount - 2);
                    for (int i = 0; i < targetIdx + 1; i++)
                        objLerps[i] = i * interval;
                    objLerps[targetIdx + 1] = objLerps[targetIdx] + targetInterval;
                    for (int i = targetIdx + 2; i < objCount; i++)
                        objLerps[i] = objLerps[i - 1] + interval;
                    break;
                }
        }

        for (int i = 0; i < objCount; i++)
        {
            var targetPos = Vector3.Lerp(leftTr.position, rightTr.position, objLerps[i]);
            var targetRot = Util.QI;
            if (objCount >= 4)
            {
                float curve = Mathf.Sqrt(Mathf.Abs(Mathf.Pow(height, 2) - Mathf.Pow(objLerps[i] - 0.5f, 2)));
                curve = height >= 0 ? curve : -curve;
                targetPos.y += curve;
                targetPos.y -= 0.13f;
                targetPos.z -= 10 * i;

                if (targetPos.y < -11f)
                {
                    targetPos.y = -11f;
                }

                targetRot = Quaternion.Slerp(leftTr.rotation, rightTr.rotation, objLerps[i]);
            }

            results.Add(new PRS(targetPos, targetRot, scale));
        }

        return results;
    }

    public void CardMouseDown(Card card)
    {
        if (card == selectCard)
        {
            if (isUseCard)
            {
                DoorUIHandler.DoorUIH.StartPlayerText(card.Item.Description);
                isDrag = true;
            }
        }
        else
        {
            selectCard = card;
            TargetCardAlignment(card);
        }
    }

    public void CardMouseUp(Card card)
    {
        if(isDrag)
        {
            if (mousePos.y >= -1.1f)
            {
                if (DoorUIHandler.DoorUIH.IsRun)
                    return;
                isDrag = false;
                isUseCard = false;
                StartCoroutine(FadeOutRemove(card.gameObject));
                myCard.Remove(card);
                SetOriginOrder();
                CardAlignment();
            }
            isDrag = false;
            CardAlignment();
        }
    }

    IEnumerator FadeOutRemove(GameObject obj)
    {
        float fillAmount = 1;
        SpriteRenderer renderer = obj.GetComponent<SpriteRenderer>();
        while(fillAmount > 0)
        {
            fillAmount -= Time.deltaTime * 1.5f;
            renderer.color = new Color(1f, 1f, 1f, fillAmount);
            yield return null;
        }
        Destroy(obj);
    }

    private void Update()
    {
        if (isDrag)
        {
            mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);

            mousePos.z = -200;
            mousePos.y += 1.5f;

            prs.pos = mousePos;
            prs.rot = Util.QI;
            prs.scale = Vector3.one * 1.2f;

            selectCard.MoveTransform(prs, true, 0.15f);
        }
    }
}
