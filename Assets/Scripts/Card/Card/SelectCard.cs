using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCard : MonoBehaviour
{
    bool isDrag;
    Vector3 mousePos;

    PRS prs;
    PRS oriPrs;

    void Start()
    {
        prs = new PRS(Vector3.zero, Util.QI, Vector3.zero * 0.33f);
        oriPrs = new PRS(Vector3.zero, Util.QI, Vector3.zero * 0.33f);
    }

    void OnMouseDown()
    {
        oriPrs.pos = transform.position;
        oriPrs.rot = transform.rotation;
        oriPrs.scale = Vector3.one * 0.33f;
        isDrag = true;
    }


    [SerializeField] SpriteRenderer card;

    public Item item;

    public void Setup(Item item)
    {
        this.item = item;

        card.sprite = item.Sprite;
    }

    public void MoveTransform(PRS prs, bool useDoteen, float dotweenTime = 0)
    {
        if (useDoteen)
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
}
