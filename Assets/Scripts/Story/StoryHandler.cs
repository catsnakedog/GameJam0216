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

    private void Start()
    {
        StageStory = new List<TextInfo>();
        idx = 0;

        foreach(TextInfo info in Data.GameData.TextData.TextInfo)
        {
            if (info.Idx == Data.GameData.InGameData.Stage)
                StageStory.Add(info);
        }

        Show();
    }

    public void Show()
    {
        if (textC != null)
        {
            StopCoroutine(textC);
            PlayerText.text = StageStory[idx].Text;
            idx++;
        }
        else
        {
            if(idx >= StageStory.Count)
            {
                UI_SceneManager.Instance.ChangeScene("InGame");
                return;
            }
            StartCoroutine(ShowText());
        }
    }

    IEnumerator ShowText()
    {
        if (StageStory[idx].Focus == "I")
        {
            PlayerPanel.SetActive(false);
            RabbitPanel.SetActive(true);
            RabbitRenderer.color = new Color(0.5f, 0.5f, 0.5f, 0.5f);
        }
        else
        {
            PlayerPanel.SetActive(true);
            RabbitPanel.SetActive(false);
            PlayerRenderer.color = new Color(0.5f, 0.5f, 0.5f, 0.5f);
        }

        PlayerRenderer.sprite = Managers.Resource.Load<Sprite>(StageStory[idx].you_image);
        RabbitRenderer.sprite = Managers.Resource.Load<Sprite>(StageStory[idx].I_image);


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
    }
}
