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

    private void Update()
    {
        if (isDrag)
        {
            mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);

            mousePos.z = -200;
            mousePos.y += 0.8f;

            prs.pos = mousePos;
            prs.rot = Util.QI;
            prs.scale = Vector3.one * 0.66f;

            MoveTransform(prs, true, 0.15f);
        }
    }

    void OnMouseDown()
    {
        oriPrs.pos = transform.position;
        oriPrs.rot = transform.rotation;
        oriPrs.scale = Vector3.one * 0.33f;
        isDrag = true;
    }

    void OnMouseUp()
    {
        if(isDrag)
        {
            isDrag = false;
            MoveTransform(oriPrs, true, 0.2f);

            if (mousePos.y - 0.8f < -3.5f)
            {
                CardHandler.instance.FindAndAddCard(item.Name);
                Destroy(gameObject);
            }
        }
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
