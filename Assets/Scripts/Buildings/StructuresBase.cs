using System;
using Interactions;
using UI;
using UnityEngine;

namespace Buildings
{
    public abstract class StructuresBase : MonoBehaviour, IInteractable
    {
        private UIManager _uiManager;
        protected bool isInteracting = false;
        protected virtual void Start()
        {
            _uiManager = FindObjectOfType<UIManager>();
        }

        public void Interact()
        {
            _uiManager.ActivateCanvas(CanvasEnum.StructureUI, true);
            isInteracting = true;
        }
    }
}