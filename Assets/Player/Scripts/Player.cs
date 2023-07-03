using System;
using UnityEngine;
using UnityEngine.InputSystem;

/* Player handles generic behaviour of a player projectile - movement, detection by colliders, etc. */

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

    private bool _isPaused;
    private bool _isInShootingPhase;
    private bool _isDying;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
        _inputManager = new InputManager();

    }
    private void OnEnable()
    {
        _inputManager.Enable();

        SubscribeToPlayerInput();

        PauseMenuManager.OnPauseClickedAction += OnTogglePause;
        PauseMenuManager.OnResumeClickedAction += OnTogglePause;
        EnemyManager.OnAllMonstersKilled += OnLevelCompleted;

    }
    private void OnDisable()
    {
        _inputManager.Disable();

        UnsubscribeToPlayerInput();

        PauseMenuManager.OnPauseClickedAction += OnTogglePause;
        PauseMenuManager.OnResumeClickedAction += OnTogglePause;
        EnemyManager.OnAllMonstersKilled -= OnLevelCompleted;

    }

    private void OnTogglePause()
    {
        _isPaused = !_isPaused;
        if (_isPaused)
        {
            UnsubscribeToPlayerInput();
        }

        else 
        {
            SubscribeToPlayerInput();
        }
    }

    private void SubscribeToPlayerInput()
    {
        _inputManager.Character.Move.performed += OnMovementPerformed;
        _inputManager.Character.Move.canceled += OnMovementCanceled;
        _inputManager.Character.Fire.performed += OnFired;
    }


    private void UnsubscribeToPlayerInput()
    {
        _inputManager.Character.Move.performed -= OnMovementPerformed;
        _inputManager.Character.Move.canceled -= OnMovementCanceled;
        _inputManager.Character.Fire.performed -= OnFired;
    }

    private void FixedUpdate()
    {
        if (_isInShootingPhase || _isDying) return;

        AdjustFacingDirection();
        _characterController.Move(_horizontalMovement * _characterSpeed * Time.fixedDeltaTime);
    }

    private void OnMovementPerformed(InputAction.CallbackContext callBackContext)
    {
        var movement = callBackContext.ReadValue<Vector2>();
        _horizontalMovement = new Vector2(movement.x, 0);
        
        _animator.SetBool(PlayerAnimationKeys.IS_RUNNING, true);
    }

    private void OnMovementCanceled(InputAction.CallbackContext callBackContext)
    {
        _horizontalMovement = Vector2.zero;
        _animator.SetBool(PlayerAnimationKeys.IS_RUNNING, false);
    }


    private void OnFired(InputAction.CallbackContext callBackContext)
    {
        if (_isInShootingPhase) return;

        Instantiate(_bullet, _gun.transform.position, Quaternion.identity);
        OnShootingStarted();
    }

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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == TagKeys.MONSTER)
        {
            OnStartedDying();
        }
    }

    private void AdjustFacingDirection()
    {
        var facingDirection = _horizontalMovement.x >= 0 ? 1 : -1;
        transform.localScale = new Vector2(facingDirection, transform.localScale.y);
    }

    public void OnStartedDying()
    {
        _isDying = true;
        _animator.SetTrigger(PlayerAnimationKeys.IS_DYING);
    }

    //used by animator
    public void OnDoneDying()
    {
        _isDying = false;
        OnPlayerDied?.Invoke();
    }

    public void OnLevelCompleted()
    {
        _animator.SetTrigger(PlayerAnimationKeys.IS_WINNING);
    }
}
