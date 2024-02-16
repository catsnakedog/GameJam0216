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
        SceneManager.LoadScene("Scenes/InGame");
    }
    public void GameOverButtionpressed()
    {
        Application.Quit();
    }
    public void OptionPressed()
    {
        OptionUI = Instantiate(Prefabs, transform);
        OptionUI.SetActive(true);
        OptionUI.transform.position = new Vector3(0, 0, 0);
    }
}
