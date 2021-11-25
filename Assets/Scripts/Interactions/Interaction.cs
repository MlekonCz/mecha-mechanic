using System;
using System.Linq;
using UnityEngine;

namespace Interactions
{
    public class Interaction : MonoBehaviour
    {
        [SerializeField] private KeyCode _interactionButton;
        [SerializeField] private Transform _mainCamera;
        [SerializeField] private float _interactionDistance;
        [SerializeField] private LayerMask _layerMaskToIgnore;

        private readonly string[] _interactionTags = {"Interactable", "Pickable", "Customer"};
        private GameObject _interactableObject;
        private ObjectPicker _objectPicker;
        private bool _pickedUpObject = false;

        private void Start()
        {
            _objectPicker = GetComponent<ObjectPicker>();
        }

        private void Update()
        {
            StartInteraction();
        }
        private void StartInteraction()
        {
            if (Input.GetKeyDown(_interactionButton))
            {
                if (_pickedUpObject == false)
                {
                    if (!CheckingForPickableObject()){return;}

                   _objectPicker.HandlePickUp(_interactableObject);
                    _pickedUpObject = true;
                }
                else
                {
                    _pickedUpObject = false;
                    _objectPicker.HandlePickUp(_interactableObject);
                }
            }
        }

        private bool CheckingForPickableObject()
        {
            RaycastHit hit; 
            if (Physics.Raycast(_mainCamera.position + _mainCamera.forward, _mainCamera.forward,
                out hit, _interactionDistance, ~_layerMaskToIgnore))
            {
                if (!_interactionTags.Contains(hit.transform.tag)){return false;}
                _interactableObject = hit.transform.gameObject;
                if (!hit.transform.CompareTag("Pickable"))
                {
                    Interact();
                    return false;
                }
                return true;
            }
            return false;
        }
        
        private void Interact() 
        {
            if (_interactableObject == null){return;}
            _interactableObject.GetComponent<IInteractable>().Interact();
        }
    }
}
    

