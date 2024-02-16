using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

public class JsonManager
{
    public void SaveJson(SaveData saveData) // 데이터를 저장하는 함수
    {
        StringBuilder sb = new StringBuilder(Application.dataPath);

        string jsonText;
        string pilePath = sb.Append("/Save").ToString();
        string savePath = sb.Append("/SaveData.json").ToString();
#if UNITY_EDITOR_WIN

#endif
#if UNITY_ANDROID
        sb.Clear();
        sb.Append(Application.persistentDataPath);
        pilePath = sb.Append("/Save").ToString();
        savePath = sb.Append("/SaveData.json").ToString();
#endif
        if (!Directory.Exists(pilePath))
        {
            Directory.CreateDirectory(pilePath);
        }
        jsonText = JsonUtility.ToJson(saveData, true);
        FileStream fileStream = new FileStream(savePath, FileMode.Create);
        byte[] bytes = Encoding.UTF8.GetBytes(jsonText);
        fileStream.Write(bytes, 0, bytes.Length);
        fileStream.Close();
    }

    public void LoadJsonData<T>(string dataName, out T data) where T : new()
    {
        if (dataName == "SaveData")
        {
            StringBuilder sb = new StringBuilder(Application.dataPath);
            string loadPath = sb.Append($"/Save/{dataName}" + ".json").ToString();
#if UNITY_EDITOR_WIN
#endif
#if UNITY_ANDROID
            sb.Clear();
            sb.Append(Application.persistentDataPath);
            loadPath = sb.Append($"/Save/{dataName}" + ".json").ToString();
#endif
            if (File.Exists(loadPath))
            {
                FileStream stream = new FileStream(loadPath, FileMode.Open);
                byte[] bytes = new byte[stream.Length];
                stream.Read(bytes, 0, bytes.Length);
                stream.Close();
                string jsonData = Encoding.UTF8.GetString(bytes);
                data = JsonUtility.FromJson<T>(jsonData);
            }
            else
            {
                data = new T();
                Debug.Log($"error_JsonManager : {dataName} Json 파일이 존재하지 않습니다.");
            }
        }
        else
        {
            TextAsset jsonData = (TextAsset)Resources.Load($"Data/{dataName}", typeof(TextAsset));

            if (jsonData.text != null)
            {
                data = JsonUtility.FromJson<T>(jsonData.ToString());
            }
            else
            {
                data = new T();
                Debug.Log($"error_JsonManager : {dataName} Json 파일이 존재하지 않습니다.");
            }
        }
    }
}
