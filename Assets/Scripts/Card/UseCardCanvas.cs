using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UseCardCanvas : MonoBehaviour
{
    void Start()
    {
        GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceCamera;
        GetComponent<Canvas>().worldCamera = Camera.main;
        GetComponent<Canvas>().sortingLayerName = "UI";
        SetAllColor();
    }

    void SetAllColor()
    {
        foreach(Image image in GetComponentsInChildren<Image>())
        {
            if(image.gameObject.name != "Panel")
                image.DOFade(1, 2);
        }
    }
}
