using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private static GameController gameControllerInstance;
    public static event Action<string, int> OnSceneLoadedAction;

    private int _playerLivesLeft = 2;

    private SceneLoader _sceneLoader;

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
        SceneManager.sceneLoaded += OnSceneLoaded;
        PauseMenuManager.OnPauseClickedAction += HaltTimeProgression;
        PauseMenuManager.OnResumeClickedAction += ResumeTimeProgression;

        EnemyManager.OnAllMonstersKilled += GoToNextLevel;

        Player.OnPlayerDied += DeterminePlayerDeathOutcome;
        Player.OnPlayerFinishedLevel += GoToNextLevel;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        PauseMenuManager.OnPauseClickedAction -= HaltTimeProgression;
        PauseMenuManager.OnResumeClickedAction -= ResumeTimeProgression;

        EnemyManager.OnAllMonstersKilled -= GoToNextLevel;

        Player.OnPlayerDied -= DeterminePlayerDeathOutcome;
        Player.OnPlayerFinishedLevel -= GoToNextLevel;
    }

    public void HaltTimeProgression()
    {
        Time.timeScale = 0.0f;
    }

    public void ResumeTimeProgression()
    {
        Time.timeScale = 1.0f;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        OnSceneLoadedAction?.Invoke(scene.name, _playerLivesLeft);
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
