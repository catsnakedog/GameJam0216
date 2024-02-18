using System.Collections;
using System.Collections.Generic;
using System.Text;
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

    List<TextInfo> StageStory;

    int idx;
    bool isActive;

    private void Start()
    {
        StageStory = new List<TextInfo>();
        idx = 0;
        isActive = false;

        int stage = Data.GameData.InGameData.Stage;
        if (Data.GameData.InGameData.Stage == 5)
        {
            switch(Data.GameData.InGameData.SuccessStack)
            {
                case 5:
                    stage = 5;
                    break;
                case 4:
                    stage = 5;
                    break;
                case 3:
                    stage = 6;
                    break;
                case 2:
                    stage = 7;
                    break;
                case 1:
                    stage = 8;
                    break;
                case 0:
                    stage = 9;
                    break;
            }
        }
        foreach(TextInfo info in Data.GameData.TextData.TextInfo)
        {
            if (info.Idx == stage)
                StageStory.Add(info);
        }

        Show();
    }

    public void Show()
    {
        if (isActive)
        {
            isActive = false;
            StopCoroutine(textC);
            if (StageStory[idx].Focus == "I")
                PlayerText.text = StageStory[idx].Text;
            else
                RabbitText.text = StageStory[idx].Text;
            idx++;
        }
        else
        {
            if(idx >= StageStory.Count)
            {
                if(Data.GameData.InGameData.Stage == 5)
                {
                    UI_SceneManager.Instance.ChangeScene("MainScene");
                    return;
                }
                UI_SceneManager.Instance.ChangeScene("InGame");
                return;
            }
            textC = StartCoroutine(ShowText());
        }
    }

    IEnumerator ShowText()
    {
        isActive = true;
        if (StageStory[idx].Focus == "I")
        {
            PlayerPanel.SetActive(false);
            RabbitPanel.SetActive(true);
            RabbitRenderer.color = new Color(0.5f, 0.5f, 0.5f, 0.5f);
            PlayerRenderer.color = new Color(1f, 1f, 1f, 1f);
        }
        else
        {
            PlayerPanel.SetActive(true);
            RabbitPanel.SetActive(false);
            PlayerRenderer.color = new Color(0.5f, 0.5f, 0.5f, 0.5f);
            RabbitRenderer.color = new Color(1f, 1f, 1f, 1f);
        }

        PlayerRenderer.sprite = Managers.Resource.Load<Sprite>(StageStory[idx].I_image);
        RabbitRenderer.sprite = Managers.Resource.Load<Sprite>(StageStory[idx].You_image);


        StringBuilder sb = new();
        foreach (char s in StageStory[idx].Text)
        {
            Managers.Sound.Play("Chat");
            sb.Append(s);
            if (StageStory[idx].Focus == "I")
                PlayerText.text = sb.ToString();
            else
                RabbitText.text = sb.ToString();
            yield return new WaitForSeconds(0.045f);
        }
        idx++;

        isActive = false;
    }
}
