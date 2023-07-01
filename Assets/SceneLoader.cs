using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader
{
    private static int MAIN_MENU_INDEX = 0;
    private static int LEVEL_ONE = 1;

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
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
}
