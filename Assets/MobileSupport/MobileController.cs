using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
