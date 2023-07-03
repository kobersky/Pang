using System;
using UnityEngine;
using UnityEngine.InputSystem;

/* PauseMenuManager handles behaviour of the pause menu triggering, 
 * button actions and reporting of pause/ resume, since it alters gameplay */
public class PauseMenuManager : MonoBehaviour
{
    [SerializeField] GameObject _buttonWrapper;

    private InputManager _inputManager;
    private SceneLoader _sceneLoader;

    public static event Action OnPauseClickedAction;
    public static event Action OnResumeClickedAction;

    private bool _isPaused;

    private void Awake()
    {
        _inputManager = new InputManager();
        _sceneLoader = new SceneLoader();

        _buttonWrapper.SetActive(false);
    }

    private void OnEnable()
    {
        _inputManager.Enable();
        _inputManager.UI.Cancel.performed += OnTogglePause;
    }

    private void OnDisable()
    {
        _inputManager.UI.Cancel.performed -= OnTogglePause;
        _inputManager.UI.Navigate.performed -= OnNavigate;

        _inputManager.Disable();
    }
    private void OnNavigate(InputAction.CallbackContext callbackContext)
    {

    }

    private void OnTogglePause(InputAction.CallbackContext callBack)
    {
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
        _isPaused = true;
        _buttonWrapper.SetActive(true);
        _inputManager.UI.Navigate.performed += OnNavigate;

        OnPauseClickedAction?.Invoke();
    }

    public void OnResumeClicked()
    {
        _isPaused = false;
        _buttonWrapper.SetActive(false);
        _inputManager.UI.Navigate.performed -= OnNavigate;

        OnResumeClickedAction?.Invoke();
    }

    public void ReturnToMainMenu()
    {
        OnResumeClickedAction?.Invoke();
        _sceneLoader.LoadMainMenuScene();
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}
