using Core;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class MainMenuCanvas : MonoBehaviour
    {
        private MainMenuManager _mainMenuManager = default;
    
        [SerializeField] private Button _startButton;
        [SerializeField] private Button _quitButton;


        private void Awake()
        {
            _mainMenuManager = FindObjectOfType<MainMenuManager>();
            _startButton.onClick.AddListener(OnStartClicked);
            _quitButton.onClick.AddListener(OnQuitClicked);
        }
        
        private void OnStartClicked()
        {
            Debug.Log($"On start button clicked. Starting game!");
            _mainMenuManager.StartGame();
        }
        private void OnQuitClicked()
        {
            _mainMenuManager.QuitGame();
        }

        
        private void OnDestroy()
        {
            _startButton.onClick.RemoveAllListeners();
        }
    }
}
