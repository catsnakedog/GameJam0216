using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UI_Slider : MonoBehaviour
{
    public Slider BGMSlider, SFXSlider;
    public Button BackButton;
    private GameObject _option;
    float BGMvalue,SFXvalue;

    private void Awake()
    {
        BGMSlider.value = Data.GameData.SaveData.OptionData.BgmVolume;
        SFXSlider.value = Data.GameData.SaveData.OptionData.SfxVolume;
    }
    private void Update()
    {
        BGMvalue = BGMSlider.value;
        SFXvalue = SFXSlider.value;
        SetBGM();
        SetSFX();
    }
    public void SetBGM()
    {
        Managers.Sound.SetAudioVolume(BGMvalue, Define.Sound.Bgm);
    }
    public void SetSFX()
    {
        Managers.Sound.SetAudioVolume(SFXvalue, Define.Sound.Sfx);
    }
    public void BackButtonpressed()
    {
        Managers.Sound.Play("Option");
        _option = GameObject.FindWithTag("Option");
        _option.SetActive(false);
        Data.GameData.SaveData.OptionData.BgmVolume = BGMSlider.value;
        Data.GameData.SaveData.OptionData.SfxVolume = SFXSlider.value;
        Managers.DataManager.Save(Data.GameData.SaveData);
        Time.timeScale = 1f;
    }
}
 