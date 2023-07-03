using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* MobileController enables/disables virtual gamepad, depending on platform */

public class MobileController : MonoBehaviour
{
    [SerializeField] GameObject _virtualGamepad;

    private void Awake()
    {
    #if UNITY_ANDROID
        _virtualGamepad.SetActive(true);   
    #else
        _virtualGamepad.SetActive(false);
    #endif
    }
}
