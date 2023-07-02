using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject _mobileController;

    private SceneLoader _sceneLoader;
    private bool _isPaused;

    private void Awake()
    {
        _sceneLoader = new SceneLoader();

    #if UNITY_ANDROID
        _mobileController.SetActive(true);   
    #else
        _mobileController.SetActive(false);
    #endif
    }

    private void OnEnable()
    {
        SubscribeToPauseMenuEvents();
        SubscribeToMainMenuEvents();
        SubscribeToPlayerEvents();
        SubscribeToLevelEvents();

    }

    private void OnDisable()
    {
        UnsubscribeFromPauseMenuEvents();
        UnsubscribeFromMainMenuEvents();
        UnsubscribeFromPlayerEvents();
        UnsubscribeFromLevelEvents();
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
        Player.OnPlayerDied += GameOver;
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
        Player.OnPlayerDied -= GameOver;
        Player.OnPlayerFinishedLevel -= GoToNextLevel;
    }

    private void UnsubscribeFromLevelEvents()
    {
        EnemyManager.OnAllMonstersKilled -= GoToNextLevel;
    }

    public void StartANewGame()
    {
        _sceneLoader.LoadFirstLevel();
    }

    public void PauseGame()
    {
        _isPaused = !_isPaused;
        Debug.Log($"INPUT_GC: PauseGame!");
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        _isPaused = !_isPaused;
        Debug.Log($"INPUT_GC: OnResume");
        Time.timeScale = 1.0f;
    }

    public void ReturnToMainMenu()
    {
        Debug.Log($"INPUT_UI: ReturnToMainMenu");
        _sceneLoader.LoadMainMenuScene();
    }

    public void QuitGame()
    {
        Debug.Log($"INPUT_UI: QuitGame");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }

    private void GameOver()
    {
        _sceneLoader.LoadGameOverScene();
    }

    private void GoToNextLevel()
    {
        _sceneLoader.LoadNextLevel();
    }

}
