using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRatioEnforcer : MonoBehaviour
{
    private Camera _mainCamera;
    private readonly float _targetAspectRatio = 16.0f / 9.0f;

    private void Awake()
    {
        _mainCamera = GetComponent<Camera>();
    }

    private void OnEnable()
    {
        ResizeCamera();
    }

    private void ResizeCamera()
    {
        float screenAspectRatio = Screen.width / (float)Screen.height;
        float scaleHeight = screenAspectRatio / _targetAspectRatio;
        float scaleWidth = 1.0f / scaleHeight;

        if (scaleHeight < 1.0f)
        {
            AdjustDisplayToLetterbox(scaleHeight);
        }
        else
        {
            AdjustDisplayToPillarbox(scaleWidth);
        }
    }

    private void AdjustDisplayToLetterbox(float scaleHeight)
    {
        // add letterbox by resizing camera's rect height and adjusting it's vertical position

        var rect = _mainCamera.rect;

        rect.width = 1.0f;
        rect.height = scaleHeight;
        rect.x = 0;
        rect.y = (1.0f - scaleHeight) / 2.0f;

        _mainCamera.rect = rect;
    }

    private void AdjustDisplayToPillarbox(float scaleWidth)
    {
        // add pillarbox by resizing camera's rect width and adjusting it's horizontal position

        var rect = _mainCamera.rect;

        rect.width = scaleWidth;
        rect.height = 1.0f;
        rect.x = (1.0f - scaleWidth) / 2.0f;
        rect.y = 0;

        _mainCamera.rect = rect;

    }
}
