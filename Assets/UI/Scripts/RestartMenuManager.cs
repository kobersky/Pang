using System;
using UnityEngine;
using UnityEngine.InputSystem;

/* RestartMenuManager handles behaviour of the generic 'start a new game/quit' menu */
public class RestartMenuManager : MonoBehaviour
{
    private InputManager _inputManager;
    private Vector2 _navigation;
    private SceneLoader _sceneLoader;

    public static event Action OnGameRestartedAction;

    private void Awake()
    {
        _inputManager = new InputManager();
        _sceneLoader = new SceneLoader();
    }

    private void OnEnable()
    {
        _inputManager.Enable();
        _inputManager.UI.Navigate.performed += OnNavigate;
    }

    private void OnDisable()
    {
        _inputManager.UI.Navigate.performed -= OnNavigate;
        _inputManager.Disable();
    }

    private void OnNavigate(InputAction.CallbackContext callbackContext)
    {
        _navigation = callbackContext.ReadValue<Vector2>();
    }

    public void StartANewGame()
    {
        OnGameRestartedAction?.Invoke();
        _sceneLoader.LoadFirstLevel();
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}
