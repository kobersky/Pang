using System;
using UnityEngine;
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
        Debug.Log("INPUTx: CALLED");
        _isPaused = !_isPaused;
        if (_isPaused)
        {
            Debug.Log("INPUTx: disabling for player");
            UnsubscribeToPlayerInput();
        }

        else 
        {
            Debug.Log("INPUTx: enabling for player");
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

    private void OnMovementPerformed(CallbackContext callBackContext)
    {
        var movement = callBackContext.ReadValue<Vector2>(); //
        _horizontalMovement = new Vector2(movement.x, 0);
        Debug.Log($"INPUT: moving! [{_horizontalMovement.x}, {_horizontalMovement.y}");
        
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


/*    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"IMPACT: PLAYER: OnTriggerEnter: tag: {other.tag}, layer: {other.gameObject.layer}");
        if (other.tag == "Monster")
        {
           // OnStartedDying();//todo:revert after test
        }
    }*/

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"IMPACT: PLAYER: OnCollisionEnter: {collision.collider.tag}");

        if (collision.collider.tag == "Monster")
        {
            Debug.Log($"IMPACT: PLAYER: OnCollisionEnter - Monster confirmed");
            OnStartedDying();//todo:revert after test
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
        OnPlayerDied?.Invoke();//TODO: revert after test
    }

    public void OnLevelCompleted()
    {
        _animator.SetTrigger(PlayerAnimationKeys.IS_WINNING);
    }
}
