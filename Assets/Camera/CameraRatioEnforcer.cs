using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRatioEnforcer : MonoBehaviour
{
    private Camera mainCamera;
    float targetAspectRatio = 16.0f / 9.0f;
    float lastScreenWidth = -1f;
    float lastScreenHeight = -1f;

    private bool DidDisplayChange => 
        Screen.width != lastScreenWidth || Screen.height != lastScreenHeight;

    private void Awake()
    {
        mainCamera = GetComponent<Camera>();
        ResizeCameraIfNeeded();
    }

    private void Update()
    {
        ResizeCameraIfNeeded();
    }

    private void ResizeCameraIfNeeded()
    {
        if (DidDisplayChange)
        {
            lastScreenWidth = Screen.width;
            lastScreenHeight = Screen.height;

            float screenAspectRatio = lastScreenWidth / lastScreenHeight;
            float scaleHeight = screenAspectRatio / targetAspectRatio;
            float scaleWidth = 1.0f / scaleHeight;

            if (scaleHeight < 1.0f)
            {
                AdjustDisplayToLetterbox(scaleHeight);
            }
            else// if (scaleHeight > 1.0f)
            {
                AdjustDisplayToPillarbox(scaleWidth);
            }
        }
    }

    private void AdjustDisplayToLetterbox(float scaleHeight)
    {
        // add letterbox by resizing camera's rect height and adjusting it's vertical position

        Rect rect = mainCamera.rect;

        rect.width = 1.0f;
        rect.height = scaleHeight;
        rect.x = 0;
        rect.y = (1.0f - scaleHeight) / 2.0f;

        mainCamera.rect = rect;
    }

    private void AdjustDisplayToPillarbox(float scaleWidth)
    {
        // add pillarbox by resizing camera's rect width and adjusting it's horozontal position

        Rect rect = mainCamera.rect;

        rect.width = scaleWidth;
        rect.height = 1.0f;
        rect.x = (1.0f - scaleWidth) / 2.0f;
        rect.y = 0;

        mainCamera.rect = rect;

    }
}
