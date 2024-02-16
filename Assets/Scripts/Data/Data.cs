using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data : MonoBehaviour
{
    static Data s_instance;
    static Data instance { get { Init(); return s_instance; } }

    public GameData _gameData;
    public static GameData GameData;

    void Start()
    {
        s_instance = gameObject.GetComponent<Data>();

        Init();
    }

    static void Init()
    {
        s_instance._gameData = Data.GameData;
    }
}