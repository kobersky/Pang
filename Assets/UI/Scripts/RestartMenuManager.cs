using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class RestartMenuManager : MonoBehaviour
{
    private InputManager _inputManager;
    private Vector2 _navigation;

    public static event Action OnStartNewGameClickedAction;
    public static event Action OnQuitClickedAction;

    private void Awake()
    {
        _inputManager = new InputManager();
    }

    private void OnEnable()
    {
        _inputManager.Enable();
        _inputManager.UI.Navigate.performed += OnNavigate;
    }

    private void OnDisable()
    {
        _inputManager.Disable();
        _inputManager.UI.Navigate.performed -= OnNavigate;
    }

    private void OnNavigate(InputAction.CallbackContext callbackContext)
    {
        _navigation = callbackContext.ReadValue<Vector2>();
        Debug.Log($"INPUT_MAIN: OnNavigate! [{_navigation.x}, {_navigation.y}");
    }

    public void OnStartNewGameClicked()
    {
        OnStartNewGameClickedAction?.Invoke();
    }

    public void OnQuitClicked()
    {
        OnQuitClickedAction?.Invoke();
    }
}
