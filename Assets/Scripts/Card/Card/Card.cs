using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Card : MonoBehaviour
{
    [SerializeField] SpriteRenderer CardSR;

    [HideInInspector] public Item Item;
    [HideInInspector] public PRS OriginPRS;
    
    public void Setup(Item item)
    {
        this.Item = item;

        CardSR.sprite = item.Sprite;
    }

    public void MoveTransform(PRS prs, bool useDoteen, float dotweenTime = 0)
    {
        if(useDoteen)
        {
            transform.DOMove(prs.pos, dotweenTime);
            transform.DORotateQuaternion(prs.rot, dotweenTime);
            transform.DOScale(prs.scale, dotweenTime);
        }
        else
        {
            transform.position = prs.pos;
            transform.rotation = prs.rot;
            transform.localScale = prs.scale;
        }
    }

    void OnMouseDown()
    {
        if (!CardHandler.instance.isUseCard)
            return;
        if (DoorUIHandler.DoorUIH.IsRun)
            return;
        CardHandler.instance.CardMouseDown(this);
    }

    void OnMouseUp()
    {
        if (!CardHandler.instance.isUseCard)
            return;
        if (DoorUIHandler.DoorUIH.IsRun)
            return;
        CardHandler.instance.CardMouseUp(this);
    }
}
