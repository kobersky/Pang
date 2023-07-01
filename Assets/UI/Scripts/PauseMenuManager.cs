using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenuManager : MonoBehaviour
{
    [SerializeField] GameObject _buttonWrapper;

    private InputManager _inputManager;
    private Vector2 _navigation;

    public static event Action OnPauseClickedAction;
    public static event Action OnResumeClickedAction;
    public static event Action OnReturnToMainMenuClickedAction;
    public static event Action OnQuitGameClickedAction;

    private bool _isPaused;

    private void Awake()
    {
        _inputManager = new InputManager();
        _buttonWrapper.SetActive(false);
    }

    private void OnEnable()
    {
        Debug.Log($"INPUT_PAUSE: OnEnable");
        _inputManager.Enable();
        _inputManager.UI.Cancel.performed += OnTogglePause;
    }

    private void OnDisable()
    {
        Debug.Log($"INPUT_PAUSE: OnDisable");
        _inputManager.UI.Cancel.performed -= OnTogglePause;
        _inputManager.UI.Navigate.performed -= OnNavigate;

        _inputManager.Disable();
    }
    private void OnNavigate(InputAction.CallbackContext callbackContext)
    {
        _navigation = callbackContext.ReadValue<Vector2>();
        Debug.Log($"INPUT_PAUSE: OnNavigate! [{_navigation.x}, {_navigation.y}");
    }

    private void OnTogglePause(InputAction.CallbackContext callBack)
    {
        Debug.Log($"INPUT_PAUSE: OnTogglePause");
        if (_isPaused)
        {
            OnResumeClicked();
        }
        else 
        {
            OnPauseClicked();
        }
    }

    public void OnPauseClicked()
    {        
        Debug.Log($"INPUT_PAUSE: OnPauseClicked");
        _isPaused = true;
        _buttonWrapper.SetActive(true);
        _inputManager.UI.Navigate.performed += OnNavigate;

        OnPauseClickedAction?.Invoke();
    }

    public void OnResumeClicked()
    {
        Debug.Log($"INPUT_PAUSE: OnResumeClicked");
        _isPaused = false;
        _buttonWrapper.SetActive(false);
        _inputManager.UI.Navigate.performed -= OnNavigate;

        OnResumeClickedAction?.Invoke();
    }

    public void OnMainMenuClicked()
    {
        OnReturnToMainMenuClickedAction?.Invoke();
    }

    public void OnQuitClicked()
    {
        OnQuitGameClickedAction?.Invoke();
    }
}
