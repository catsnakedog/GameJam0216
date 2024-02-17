using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BG : MonoBehaviour
{
    public string Name;

    void Start()
    {
        if (Name == "Main")
            Managers.Sound.Play("bgm_original", Define.Sound.Bgm);
        else if (Name == "InGame")
        {
            PlaySwitchBG(Data.GameData.InGameData.SuccessStack);
        }
        else
        {
            PlaySwitchBG(Data.GameData.InGameData.SuccessStack);
        }
    }

    void PlaySwitchBG(int num)
    {
        switch (num)
        {
            case 0:
                Managers.Sound.Play("bgm_original", Define.Sound.Bgm);
                break;
            case 1:
                Managers.Sound.Play("bgm_glitch0", Define.Sound.Bgm);
                break;
            case 2:
                Managers.Sound.Play("bgm_glitch1", Define.Sound.Bgm);
                break;
            case 3:
                Managers.Sound.Play("bgm_glitch2", Define.Sound.Bgm);
                break;
            case 4:
                Managers.Sound.Play("bgm_glitch3", Define.Sound.Bgm);
                break;
            case 5:
                Managers.Sound.Play("bgm_glitch4", Define.Sound.Bgm);
                break;
        }
    }
}
