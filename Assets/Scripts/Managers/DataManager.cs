using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class DataManager
{
    JsonManager _jsonManager;
    public GameData GameData;

    void Start()
    {
        Init();
    }

    public void Init()
    {
        GameObject go = GameObject.Find("@Data");
        if (go == null)
        {
            GameData gameData = new GameData();

            _jsonManager = new JsonManager();

            _jsonManager.LoadJsonData<SaveData>("SaveData", out gameData.SaveData);

            go = new GameObject { name = "@Data" };
            UnityEngine.Object.DontDestroyOnLoad(go);
            Data.GameData = gameData;
            go.AddComponent<Data>();
            SoundSetting();
        }
    }

    public void SoundSetting()
    {
        Managers.Sound.SetAudioVolume(Data.GameData.SaveData.OptionData.SfxVolume, Define.Sound.Sfx);
        Managers.Sound.SetAudioVolume(Data.GameData.SaveData.OptionData.BgmVolume, Define.Sound.Bgm);
    }

    public void Save(SaveData saveData)
    {
        _jsonManager.SaveJson(saveData);
    }
}