using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/* SceneLoader loads scenes */

public class SceneLoader
{
    private const int MAIN_MENU_INDEX = 0;
    private const int FIRST_LEVEL = 1;
    private const int LAST_LEVEL = 3;
    private const int VICTORY_INDEX = 4;
    private const int GAME_OVER_INDEX = 5;

    private int CurrentSceneIndex => SceneManager.GetActiveScene().buildIndex;

    public void LoadMainMenuScene()
    {
        SceneManager.LoadScene(MAIN_MENU_INDEX);
    }

    public void LoadFirstLevel()
    {
        SceneManager.LoadScene(FIRST_LEVEL);
    }

    public void LoadNextLevel()
    {
        switch (CurrentSceneIndex)
        { 
            case LAST_LEVEL:
                SceneManager.LoadScene(VICTORY_INDEX);
                break;
            default:
                SceneManager.LoadScene(CurrentSceneIndex + 1);
                break;
        }
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(CurrentSceneIndex);
    }

    public void LoadGameOverScene()
    {
        SceneManager.LoadScene(GAME_OVER_INDEX);
    }
}
