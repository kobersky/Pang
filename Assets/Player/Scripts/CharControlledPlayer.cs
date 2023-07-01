using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XInput;
using static UnityEngine.InputSystem.InputAction;

public class CharControlledPlayer : MonoBehaviour
{
    private CharacterController _characterController;
    private Vector2 _horizontalMovement;
    private InputManager _inputManager;

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

    private void OnMovementPerformed(InputAction.CallbackContext callBackContext)
    {
        _horizontalMovement = callBackContext.ReadValue<Vector2>();
        Debug.Log($"INPUT: moving! [{_horizontalMovement.x}, {_horizontalMovement.y}");
    }

    private void OnMovementCanceled(InputAction.CallbackContext callBackContext)
    {
        _horizontalMovement = Vector2.zero;
        Debug.Log($"INPUT: canceled! [{_horizontalMovement.x}, {_horizontalMovement.y}");
    }


    private void OnFired(CallbackContext callBackContext)
    {
        Debug.Log($"INPUT: firing!");
    }

    /*    private void OnMovementSarted(InputAction.CallbackContext callBackContext)
        {
            _horizontalMovement = callBackContext.ReadValue<Vector2>();
            Debug.Log($"INPUT: moving! [{_horizontalMovement.x}, {_horizontalMovement.y}");
        }*/

    private void FixedUpdate()
    {
        _characterController.Move(_horizontalMovement * 5 * Time.fixedDeltaTime);
    }

/*    public void OnFire(InputValue inputValue)
    {

        Debug.Log($"INPUT: firing!");
    }

    public void OnMove(InputValue inputValue)
    {
        _horizontalMovement = inputValue.Get<Vector2>();
        Debug.Log($"INPUT: moving! [{_horizontalMovement.x}, {_horizontalMovement.y}");
    }*/

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"X: OnTriggerEnter: tag: {other.tag}, layer: {other.gameObject.layer}");
    }
}
