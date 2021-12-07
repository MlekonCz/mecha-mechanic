using Core;
using UnityEngine;


namespace UI
{
    //to work properly needs to start testing game from MainMenu Scene
    public enum CanvasEnum
    {
        menuUI,
        gameUI,
        computerUI,
        customerUI,
        StructureUI,
        
    }
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private GameObject[] Canvases;

        [SerializeField]private CanvasEnum lastCanvas = CanvasEnum.menuUI;
        
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

        public void ActivateCanvas(CanvasEnum newCanvas, bool lockMouse)
        {
            Canvases[(int) lastCanvas].SetActive(false);
            lastCanvas = newCanvas;
            Canvases[(int) newCanvas].SetActive(true);

            FindObjectOfType<GameManager>().LockMovement(lockMouse);
        }
        private void SetupUIManager()
        {
            _mainMenuManager.OnGameStarted += InitGameCanvas;
            _mainMenuManager.OnGameExit += InitMenuCanvas;
        }
        private void InitGameCanvas()
        {
            Canvases[(int) CanvasEnum.menuUI].SetActive(false);
            Canvases[(int) CanvasEnum.gameUI].SetActive(true);
            lastCanvas = CanvasEnum.gameUI;
        }

        private void InitMenuCanvas()
        {
            Canvases[(int) CanvasEnum.gameUI].SetActive(false);
            Canvases[(int) CanvasEnum.menuUI].SetActive(true);
            lastCanvas = CanvasEnum.menuUI;
        }
        private void Update()
        {
            HandlePauseScreen();
        }

        private void HandlePauseScreen()
        {
            return;
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (!Canvases[0].activeSelf)
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