using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;
using static UnityEngine.InputSystem.InputAction;

public class CharControlledPlayer : MonoBehaviour
{
    [SerializeField] float _characterSpeed = 5;
    [SerializeField] GameObject _gun;
    [SerializeField] GameObject _bullet;

    private CharacterController _characterController;
    private Vector2 _horizontalMovement;
    private InputManager _inputManager;

    public static event Action OnPlayerDied;
    public static event Action OnPlayerFinishedLevel;


    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _inputManager = new InputManager();

    }
    private void OnEnable()
    {
        _inputManager.Enable();

        _inputManager.Character.Move.performed += OnMovementPerformed;
        _inputManager.Character.Move.canceled += OnMovementCanceled;
        _inputManager.Character.Fire.performed += OnFired;

    }

    private void OnDisable()
    {
        _inputManager.Disable();

        _inputManager.Character.Move.performed -= OnMovementPerformed;
        _inputManager.Character.Move.canceled -= OnMovementCanceled;
        _inputManager.Character.Fire.performed -= OnFired;
    }

    private void OnMovementPerformed(CallbackContext callBackContext)
    {
        _horizontalMovement = callBackContext.ReadValue<Vector2>();
        Debug.Log($"INPUT: moving! [{_horizontalMovement.x}, {_horizontalMovement.y}");
    }

    private void OnMovementCanceled(CallbackContext callBackContext)
    {
        _horizontalMovement = Vector2.zero;
        Debug.Log($"INPUT: canceled! [{_horizontalMovement.x}, {_horizontalMovement.y}");
    }


    private void OnFired(CallbackContext callBackContext)
    {
        Debug.Log($"INPUT: firing!");
        Instantiate(_bullet, _gun.transform.position, Quaternion.identity);

    }

    private void FixedUpdate()
    {
        _characterController.Move(_horizontalMovement * _characterSpeed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"X: OnTriggerEnter: tag: {other.tag}, layer: {other.gameObject.layer}");
        if (other.tag == "Monster")
        {
          //  OnPlayerDied?.Invoke();//TODO: revert after test
        }
        
    }
}
