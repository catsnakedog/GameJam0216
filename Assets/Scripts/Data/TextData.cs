using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TextData
{
    public List<TextInfo> TextInfo;

    public TextData(List<TextInfo> textInfo)
    {
        TextInfo = textInfo;
    }

    public TextData()
    {
        TextInfo = new();
    }
}

[System.Serializable]
public class TextInfo
{
    public int Idx;
    public string Focus;
    public string I_image;
    public string you_image;
    public string Text;

    public TextInfo(int idx, string focus, string i_image, string you_image, string text)
    {
        Idx = idx;
        Focus = focus;
        I_image = i_image;
        this.you_image = you_image;
        Text = text;
    }
    public TextInfo()
    {
        Idx = 0;
        Focus = "I";
        I_image = "";
        you_image = "";
        Text = "";
    }
}