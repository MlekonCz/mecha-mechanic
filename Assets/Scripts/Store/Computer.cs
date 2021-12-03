using System;
using Interactions;
using UI;
using UnityEngine;

namespace Store
{
    public class Computer : MonoBehaviour, IInteractable
    {
        private UIManager _uiManager;
        private void Start()
        {
            _uiManager = FindObjectOfType<UIManager>();
        }

        public void Interact()
        {
            _uiManager.ActivateCanvas(CanvasEnum.computerUI,true);
        }

    }
}
