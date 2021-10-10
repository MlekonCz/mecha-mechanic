using System;
using Core;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private SceneLoader _loader = default;

    public Action OnGameStarted;
    public Action OnGameExit;

    private void Awake()
    {
        _loader = GetComponent<SceneLoader>();
    }

    public void StartGame()
    {
        SceneManager.LoadSceneAsync((int) ScenesIndexes.GameScene, LoadSceneMode.Additive);
        OnGameStarted?.Invoke();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}