using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UI_Slider : MonoBehaviour
{
    public Slider BGMSlider, SFXSlider;
    float BGMvalue,SFXvalue;

    private void Awake()
    {
        BGMSlider.value = Managers.Sound.GetAudioSource(Define.Sound.Bgm).volume;
        SFXSlider.value = Managers.Sound.GetAudioSource(Define.Sound.Sfx).volume;
    }
    private void Update()
    {
        BGMvalue = BGMSlider.value;
        SFXvalue = SFXSlider.value;
    }
    public void SetBGM()
    {
        Managers.Sound.SetAudioVolume(BGMvalue, Define.Sound.Bgm);
    }
    public void SetSFX()
    {
        Managers.Sound.SetAudioVolume(SFXvalue, Define.Sound.Sfx);
    }
}
