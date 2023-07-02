using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private static GameController gameControllerInstance;
    public static event Action<String, int> OnSceneLoadedAction;

    private int _playerLivesLeft = 2;

    private SceneLoader _sceneLoader;
    private bool _isPaused;

    private const float WAITING_TIME_BEFORE_LEVELS = 2;

    private void Awake()
    {
        if (gameControllerInstance != null)
        {
            Debug.Log("GameController: Awake - Destroy");
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("GameController - Awake - DontDestroyOnLoad");
            gameControllerInstance = this;
            _sceneLoader = new SceneLoader();
            DontDestroyOnLoad(this);
        }
    }

    private void OnEnable()
    {
        SubscribeToPauseMenuEvents();
        SubscribeToMainMenuEvents();
        SubscribeToPlayerEvents();
        SubscribeToLevelEvents();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        UnsubscribeFromPauseMenuEvents();
        UnsubscribeFromMainMenuEvents();
        UnsubscribeFromPlayerEvents();
        UnsubscribeFromLevelEvents();
        SceneManager.sceneLoaded -= OnSceneLoaded;

    }


    private void SubscribeToMainMenuEvents()
    {
        RestartMenuManager.OnStartNewGameClickedAction += StartANewGame;
        RestartMenuManager.OnQuitClickedAction += QuitGame;
    }

    private void SubscribeToPauseMenuEvents()
    {
        PauseMenuManager.OnPauseClickedAction += PauseGame;
        PauseMenuManager.OnResumeClickedAction += ResumeGame;
        PauseMenuManager.OnReturnToMainMenuClickedAction += ReturnToMainMenu;
        PauseMenuManager.OnQuitGameClickedAction += QuitGame;
    }

    private void SubscribeToPlayerEvents()
    {
        Player.OnPlayerDied += DeterminePlayerDeathOutcome;
        Player.OnPlayerFinishedLevel += GoToNextLevel;
    }

    private void SubscribeToLevelEvents()
    {
        EnemyManager.OnAllMonstersKilled += GoToNextLevel;
    }

    private void UnsubscribeFromMainMenuEvents()
    {
        RestartMenuManager.OnStartNewGameClickedAction -= StartANewGame;
        RestartMenuManager.OnQuitClickedAction -= QuitGame;
    }

    private void UnsubscribeFromPauseMenuEvents()
    {
        PauseMenuManager.OnPauseClickedAction -= PauseGame;
        PauseMenuManager.OnResumeClickedAction -= ResumeGame;
        PauseMenuManager.OnReturnToMainMenuClickedAction -= ReturnToMainMenu;
        PauseMenuManager.OnQuitGameClickedAction -= QuitGame;
    }

    private void UnsubscribeFromPlayerEvents()
    {
        Player.OnPlayerDied -= DeterminePlayerDeathOutcome;
        Player.OnPlayerFinishedLevel -= GoToNextLevel;
    }

    private void UnsubscribeFromLevelEvents()
    {
        EnemyManager.OnAllMonstersKilled -= GoToNextLevel;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        OnSceneLoadedAction?.Invoke(scene.name, _playerLivesLeft);
    }

    public void StartANewGame()
    {
        _sceneLoader.LoadFirstLevel();
    }

    public void PauseGame()
    {
        _isPaused = !_isPaused;
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        _isPaused = !_isPaused;
        Time.timeScale = 1.0f;
    }

    public void ReturnToMainMenu()
    {
        _sceneLoader.LoadMainMenuScene();
    }

    public void QuitGame()
    {
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #endif
        Application.Quit();
    }

    private void DeterminePlayerDeathOutcome()
    {
        _playerLivesLeft--;
        if (_playerLivesLeft < 0)
        {
            _sceneLoader.LoadGameOverScene();
        }
        else 
        {
            _sceneLoader.RestartLevel();
        }
    }

    private void GoToNextLevel()
    {
        StartCoroutine(WaitAndLoadNextLevel());
    }

    private IEnumerator WaitAndLoadNextLevel()
    {
        yield return new WaitForSeconds(WAITING_TIME_BEFORE_LEVELS);

        _sceneLoader.LoadNextLevel();
    }
}