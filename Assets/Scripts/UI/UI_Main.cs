using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UI_Main : MonoBehaviour
{
    public Button StartButton,GameOverButton,Option;
    public GameObject Prefabs;
    private GameObject OptionUI;
    

    public void StartButtonpressed()
    {
        Data.GameData.InGameData.Stage = 0;
        Data.GameData.InGameData.SuccessStack = 0;
        Data.GameData.InGameData.MyItem.Clear();
        Data.GameData.SaveData.Stage = 0;
        Data.GameData.SaveData.SuccessStack = 0;
        Data.GameData.SaveData.MyItem.Clear();
        Managers.Sound.Play("Click");
        UI_SceneManager.Instance.ChangeScene("Story");
    }
    public void ContinueButtonpressed()
    {
        Data.GameData.InGameData.Stage = Data.GameData.SaveData.Stage;
        Data.GameData.InGameData.SuccessStack = Data.GameData.SaveData.SuccessStack;
        Data.GameData.InGameData.MyItem = Data.GameData.SaveData.MyItem;
        Managers.Sound.Play("Click");
        UI_SceneManager.Instance.ChangeScene("Story");
    }
    public void GameOverButtionpressed()
    {
        Managers.Sound.Play("Click");
        Application.Quit();
    }
    public void OptionPressed()
    {
        Managers.Sound.Play("Option");
        OptionUI = Instantiate(Prefabs, transform);
        OptionUI.SetActive(true);
        OptionUI.transform.position = new Vector3(0, 0, 0);
        Time.timeScale = 0f;
    }
}
