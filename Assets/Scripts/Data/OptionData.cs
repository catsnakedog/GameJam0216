using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class OptionData
{
    public float BgmVolume;
    public float SfxVolume;

    public OptionData(float bgmVolume = 0.5f, float sfxVolume = 0.5f)
    {
        BgmVolume = bgmVolume;
        SfxVolume = sfxVolume;
    }
}
