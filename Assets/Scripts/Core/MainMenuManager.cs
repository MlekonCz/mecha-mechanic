using System;
using UnityEngine;

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
            _loader.LoadScene(ScenesIndexes.GameScene, ScenesIndexes.MainMenu);
            
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}