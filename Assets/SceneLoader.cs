using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader
{
    private const int MAIN_MENU_INDEX = 0;
    private const int LEVEL_ONE = 1;
    private const int LEVEL_TWO = -1;
    private const int LEVEL_THREE = -1;
    private const int VICTORY_INDEX = 2;
    private const int GAME_OVER_INDEX = 3;

    public void LoadMainMenuScene()
    {
        SceneManager.LoadScene(MAIN_MENU_INDEX);
    }

    public void LoadFirstLevel()
    {
        SceneManager.LoadScene(LEVEL_ONE);
    }

    public void LoadNextLevel()
    {
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        switch (currentSceneIndex)
        { 
            case LEVEL_ONE:
                SceneManager.LoadScene(VICTORY_INDEX);
                break;
            default:
                SceneManager.LoadScene(currentSceneIndex + 1);
                break;

        }
    }

    public void LoadGameOverScene()
    {
        SceneManager.LoadScene(GAME_OVER_INDEX);
    }
}
