using System;
using UI;
using UnityEngine;

namespace Interactions
{
    public class Computer : MonoBehaviour, IInteractable
    {
        private UIManager _uiManager;
        public event Action onComputerAccess;
        private void Start()
        {
            _uiManager = FindObjectOfType<UIManager>();
        }

        public void Use()
        {
            _uiManager.ActivateCanvas(CanvasEnum.computerUI);
            onComputerAccess?.Invoke();
        }
        
    }
}
