using System;
using TMPro;
using UnityEngine;

public class GamePlayInfo : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _levelName;
    [SerializeField] TextMeshProUGUI _livesLeft;

    void OnEnable()
    {
        Debug.Log("LOADER: OnEnable called");
        GameController.OnSceneLoadedAction += UpdatePanel;
    }

    void OnDisable()
    {
        Debug.Log("LOADER: OnDisable");
        GameController.OnSceneLoadedAction -= UpdatePanel;
    }
    private void UpdatePanel(string sceneName, int livesLeft)
    {
        _levelName.text = sceneName;
        _livesLeft.text = livesLeft.ToString();
    }
}
