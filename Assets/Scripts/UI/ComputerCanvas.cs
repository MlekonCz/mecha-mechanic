using System;
using Store;
using UnityEngine;

namespace UI
{
    
    public class ComputerCanvas : MonoBehaviour
    {
        private Computer _computer;
        private UIManager _uiManager;
        
        [SerializeField] private GameObject computerScreen = null;
        [SerializeField] private KeyCode keyCodeToLeave = KeyCode.Q;

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
            _uiManager.ActivateCanvas(CanvasEnum.gameUI, false);
          
        }

    }
}
