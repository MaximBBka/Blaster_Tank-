using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupCamera : MonoBehaviour
{
    [SerializeField] private Camera cam;

    void Update()
    {
        // �������� ������� ����������� ������ ������
        float targetAspect = 5.0f / 16.0f; // ������� ����������� ������
        float windowAspect = (float)Screen.width / (float)Screen.height;
        float scaleHeight = windowAspect / targetAspect;

        // ����������� ������ � ��������� ������� ��������� ������
        Rect rect = cam.rect;

        if (scaleHeight < 1.0f)
        {
            rect.width = 1.0f;
            rect.height = scaleHeight;
            rect.x = 0;
            rect.y = (1.0f - scaleHeight) / 2.0f;
        }
        else
        {
            float scaleWidth = 2.0f / scaleHeight;
            rect.width = scaleWidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scaleWidth) / 2.0f;
            rect.y = 0;
        }

        //rect.width = Screen.width;
        //rect.height = Screen.height;
        //rect.x = 0.5f;
        //rect.y = 0.5f;

        cam.rect = rect;
    }
}
