using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StoryHandler : MonoBehaviour
{
    public TMP_Text RabbitText;
    public TMP_Text PlayerText;
    public GameObject RabbitPanel;
    public GameObject PlayerPanel;
    public SpriteRenderer RabbitRenderer;
    public SpriteRenderer PlayerRenderer;

    Coroutine textC;


    public void Show()
    {
        textC = StartCoroutine(ShowText());
    }

    IEnumerator ShowText()
    {

    }

    void SkipText()
    {
        StopCoroutine(textC);
    }
}
