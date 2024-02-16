using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Managers : MonoBehaviour
{
    static Managers s_instance;
    static Managers instance { get { Init(); return s_instance; } }

    ResourceManager _resource = new ResourceManager();
    SoundManager _sound = new SoundManager();
    DataManager _dataManager = new DataManager();
    public static ResourceManager Resource { get { return instance._resource; } }
    public static SoundManager Sound { get { return instance._sound; } }
    public static DataManager DataManager { get { return instance._dataManager; } }

    void Awake()
    {
        Init();
    }

    static void Init()
    {
        if (s_instance == null)
        {
            GameObject go = GameObject.Find("@Managers");
            if (go == null)
            {
                go = new GameObject { name = "@Managers" };
                go.AddComponent<Managers>();
            }

            DontDestroyOnLoad(go);
            s_instance = go.GetComponent<Managers>();

            s_instance._sound.Init();
            s_instance._dataManager.Init();
            s_instance._resource.Init();
        }
    }
}
