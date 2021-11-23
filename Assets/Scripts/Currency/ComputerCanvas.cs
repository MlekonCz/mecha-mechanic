using System;
using Interactions;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace Currency
{
    public class ComputerCanvas : MonoBehaviour
    {
        private Computer _computer;
        private UIManager _uiManager;
        
        [SerializeField] private GameObject computerScreen = null;
        [SerializeField] private KeyCode keyCodeToLeave = KeyCode.Q;
        private Color orange;

        [Header("Tabs")] 
        [SerializeField] private GameObject MainScreen;
        [SerializeField] private GameObject ShopScreen;

        [Header("Booleans")]
        private bool isActiveWindow = false;

        private void Start()
        {
            _uiManager = FindObjectOfType<UIManager>();
        }

        private void OnEnable()
        {
            _computer = FindObjectOfType<Computer>();
            _computer.onComputerAccess += AccessComputer;
        }

        private void Update()
        {
           
            if (Input.GetKeyDown(keyCodeToLeave))
            {
                CloseComputer();
            }
        }
        private void CloseComputer()
        {
            Cursor.lockState = CursorLockMode.Locked;
            _uiManager.ActivateCanvas(CanvasEnum.gameUI);
          
        }
        private void AccessComputer()
        {
            Cursor.lockState = CursorLockMode.None;
        }

    }
}
