using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;
using static UnityEngine.InputSystem.InputAction;

public class Player : MonoBehaviour
{
    [SerializeField] float _characterSpeed = 5;
    [SerializeField] GameObject _gun;
    [SerializeField] GameObject _bullet;

    private CharacterController _characterController;
    private Animator _animator;

    private Vector2 _horizontalMovement;
    private InputManager _inputManager;

    public static event Action OnPlayerDied;
    public static event Action OnPlayerFinishedLevel;

    private bool _isInShootingPhase;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
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
        if (_isInShootingPhase) return;

        _horizontalMovement = callBackContext.ReadValue<Vector2>();
        Debug.Log($"INPUT: moving! [{_horizontalMovement.x}, {_horizontalMovement.y}");

        AdjustFacingDirection();

        _animator.SetBool(PlayerAnimationKeys.IS_RUNNING, true);

    }

    private void OnMovementCanceled(CallbackContext callBackContext)
    {
        _horizontalMovement = Vector2.zero;
        Debug.Log($"INPUT: canceled! [{_horizontalMovement.x}, {_horizontalMovement.y}");
        _animator.SetBool(PlayerAnimationKeys.IS_RUNNING, false);
    }


    private void OnFired(CallbackContext callBackContext)
    {
        if (_isInShootingPhase) return;

        Debug.Log($"INPUT: firing!");
        Instantiate(_bullet, _gun.transform.position, Quaternion.identity);
        OnShootingStarted();
    }

    //used by animator
    public void OnShootingStarted()
    {
        _isInShootingPhase = true;
        _animator.SetBool(PlayerAnimationKeys.IS_SHOOTING, true);

    }

    //used by animator
    public void OnShootingFinished()
    {
        _isInShootingPhase = false;
        _animator.SetBool(PlayerAnimationKeys.IS_SHOOTING, false);
    }

    private void FixedUpdate()
    {
        _characterController.Move(_horizontalMovement * _characterSpeed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"IMPACT: OnTriggerEnter (player): tag: {other.tag}, layer: {other.gameObject.layer}");
        if (other.tag == "Monster")
        {
           // OnPlayerDied?.Invoke();//TODO: revert after test
            _animator.SetTrigger(PlayerAnimationKeys.IS_DYING);

        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"IMPACT: OnCollisionEnter (player): {collision.collider.tag}");

        if (collision.collider.tag == "Monster")
        {
            Debug.Log($"IMPACT: OnCollisionEnter (player) - Monster confirmed");
            // OnPlayerDied?.Invoke();//TODO: revert after test
            _animator.SetTrigger(PlayerAnimationKeys.IS_DYING);
        }
    }

    private void AdjustFacingDirection()
    {
        var facingDirection = _horizontalMovement.x >= 0 ? 1 : -1;
        transform.localScale = new Vector2(facingDirection, transform.localScale.y);
    }


}
