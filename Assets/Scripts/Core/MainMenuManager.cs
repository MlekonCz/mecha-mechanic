using System;
using Scenes;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core
{
    public class MainMenuManager : MonoBehaviour
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
            OnGameStarted?.Invoke();
            int buildIndex = SceneManager.GetActiveScene().buildIndex;
            _loader.LoadScene(ScenesIndexes.GameScene, buildIndex);
            
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}