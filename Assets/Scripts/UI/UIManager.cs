using System;
using UnityEngine;
using UnityEngine.UI;


namespace UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private GameObject _menuUi;
        [SerializeField] private GameObject _gameUi;

        private MainMenu _mainMenu = default;
        private bool _initialized = false;
        private bool _gameIsRunning = false;

        public Button StartButton;
        public Button QuitButton;

        private void Awake()
        {
            _mainMenu = FindObjectOfType<MainMenu>();
            _initialized = _mainMenu != null;
            if (!_initialized)
            {
                return;
            }

            SetupUIManager();
            InitMenuCanvas();
        }

        private void SetupUIManager()
        {
            StartButton.onClick.AddListener(OnStartClicked);
            QuitButton.onClick.AddListener(OnQuitClicked);

            _mainMenu.OnGameStarted += InitGameCanvas;
            _mainMenu.OnGameExit += InitMenuCanvas;
        }

        private void OnQuitClicked()
        {
            _mainMenu.QuitGame();
        }

        /// <summary>
        /// Funkce, ktera se zavola, kdyz na UI vrstve kliknu na Start Game, dale jde do Main Menu
        /// </summary>
        private void OnStartClicked()
        {
            Debug.Log($"On start button clicked. Starting game!");
            _mainMenu.StartGame();
            _gameIsRunning = true;
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

        private void OnDestroy()
        {
            StartButton.onClick.RemoveAllListeners();
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