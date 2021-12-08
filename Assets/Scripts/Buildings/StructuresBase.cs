using System;
using Interactions;
using MechParts;
using UI;
using UnityEngine;

namespace Buildings
{
    public abstract class StructuresBase : MonoBehaviour, IInteractable
    {
        private StructureCanvas _structureCanvas;
        protected UIManager _uiManager;
        protected bool isInteracting = false;
        
        protected virtual void Start()
        {
            _uiManager = FindObjectOfType<UIManager>();
            _structureCanvas = FindObjectOfType<StructureCanvas>();
        }

        protected void SubscribeToCanvas(GameObject MechPart)
        {
            if (_structureCanvas == null)
            {
                _structureCanvas = FindObjectOfType<StructureCanvas>();
            }

            if (MechPart != null)
            {
                BodyPartBase bodyPart = MechPart.GetComponent<BodyPartBase>();
                _structureCanvas.UpdateStatusOfPart(bodyPart.GetCurrentState(), bodyPart.GetPartName());
            }
            _structureCanvas.onLeaveClicked += CloseUI;
            _structureCanvas.onRemoveClicked += RemoveItem;
        }
        protected void UnSubscribeFromCanvas()
        {
            _structureCanvas.onLeaveClicked -= CloseUI;
            _structureCanvas.onRemoveClicked -= RemoveItem;
        }

        protected virtual void OpenUI()
        {
            _uiManager.ActivateCanvas(CanvasEnum.StructureUI, true);
            SubscribeToCanvas(null);
        }
        protected virtual void OpenUI(GameObject MechPart)
        {
            _uiManager.ActivateCanvas(CanvasEnum.StructureUI, true);
            SubscribeToCanvas(MechPart);
        }
        protected virtual void CloseUI()
        {
            _uiManager.ActivateCanvas(CanvasEnum.gameUI, false);
            UnSubscribeFromCanvas();
        }

        protected virtual void RemoveItem()
        {
        }
        
        public void Interact()
        { 
            OpenUI();
            isInteracting = true;
        }
        
        //add method for placing buildings

    }
}