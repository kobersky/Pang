using System;
using TMPro;
using UnityEngine;

/* GamePlayInfo handles displaying of info relevant to current level 
 * currently, player lives and level name */

public class GamePlayInfo : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _levelName;
    [SerializeField] TextMeshProUGUI _livesLeft;

    void OnEnable()
    {
        GameController.OnSceneLoadedAction += UpdatePanel;
    }

    void OnDisable()
    {
        GameController.OnSceneLoadedAction -= UpdatePanel;
    }

    private void UpdatePanel(string sceneName, int livesLeft)
    {
        _levelName.text = sceneName;
        _livesLeft.text = livesLeft.ToString();
    }
}
