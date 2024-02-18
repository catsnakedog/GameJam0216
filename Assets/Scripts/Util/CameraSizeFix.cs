using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSizeFix : MonoBehaviour
{
    int BeforeWidth;
    int BeforeHeight;
    Camera mainCamera;

    void Awake()
    {
        mainCamera = GetComponent<Camera>();
        BeforeWidth = Screen.width;
        BeforeHeight = Screen.height;
        SetResolution();
    }

    void SetResolution()
    {
        int setWidth = 1080; // ����� ���� �ʺ�
        int setHeight = 2400; // ����� ���� ����

        int deviceWidth = Screen.width; // ��� �ʺ� ����
        int deviceHeight = Screen.height; // ��� ���� ����
        BeforeWidth = deviceWidth;
        BeforeHeight = deviceHeight;

        if ((float)setWidth / setHeight < (float)deviceWidth / deviceHeight) // ����� �ػ� �� �� ū ���
        {
            float newWidth = ((float)setWidth / setHeight) / ((float)deviceWidth / deviceHeight); // ���ο� �ʺ�
            mainCamera.rect = new Rect((1f - newWidth) / 2f, 0f, newWidth, 1f); // ���ο� Rect ����
        }
        else // ������ �ػ� �� �� ū ���
        {
            float newHeight = ((float)deviceWidth / deviceHeight) / ((float)setWidth / setHeight); // ���ο� ����
            mainCamera.rect = new Rect(0f, (1f - newHeight) / 2f, 1f, newHeight); // ���ο� Rect ����
        }
    }
}
