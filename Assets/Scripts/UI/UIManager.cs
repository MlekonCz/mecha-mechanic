using System;
using System.Collections;
using Core;
using UnityEngine;
using UnityEngine.UI;


namespace UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private GameObject _menuUi;
        [SerializeField] private GameObject _gameUi;

        private MainMenuManager _mainMenuManager = default;
        private bool _initialized = false;
        private bool _gameIsRunning = false;

        private void Awake()
        {
            _mainMenuManager = FindObjectOfType<MainMenuManager>();
            _initialized = _mainMenuManager != null;
            if (!_initialized)
            {
                return;
            }

            SetupUIManager();
            InitMenuCanvas();
        }

        private void SetupUIManager()
        {
            _mainMenuManager.OnGameStarted += InitGameCanvas;
            _mainMenuManager.OnGameExit += InitMenuCanvas;
        }
        private void InitGameCanvas()
        {
            _menuUi.SetActive(false);
            _gameUi.SetActive(true);
        }

        private void InitMenuCanvas()
        {
            _gameUi.SetActive(false);
            _menuUi.SetActive(true);
        }

        private void Update()
        {
            HandlePauseScreen();
        }

        private void HandlePauseScreen()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (!_menuUi.activeSelf)
                {
                    InitMenuCanvas();
                }
                else
                {
                    InitGameCanvas();
                }
            }
        }
    }
}