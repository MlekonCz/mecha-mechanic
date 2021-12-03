using System;
using System.Linq;
using Buildings;
using MechParts;
using Unity.VisualScripting;
using UnityEngine;

namespace Interactions
{
    public class Interaction : MonoBehaviour
    {
        [SerializeField] private KeyCode _interactionButton = KeyCode.E;
        [SerializeField] private KeyCode _insertKey = KeyCode.F;
      
        [SerializeField] private Transform _mainCamera;
        [SerializeField] private LayerMask _layerMaskToIgnore;
        
        [SerializeField] private float _interactionDistance;
        [SerializeField] private float liftingPower = 10f;
        
        private IItemInserter _itemInserter = null;
        private GameObject _interactableObject;
        private ObjectPicker _objectPicker;
        
        private readonly string[] _interactionTags = {"Interactable", "Pickable", "Customer"};
        private bool _pickedUpObject = false;
        private bool itemCanBeInserted = false;
        
        private void Awake()
        {
            _objectPicker = GetComponent<ObjectPicker>();
        }
        private void Update()
        {
            StartInteraction();
            InsertItem();
        }
        private void StartInteraction()
        {
            if (Input.GetKeyDown(_interactionButton))
            {
                if (_pickedUpObject == false)
                {
                    if (!CheckingForPickableObject()){return;}

                    SubscribeToItem(); 
                    HandlePickUp(true);
                }
                else
                {
                    if (!_interactableObject.GetComponent<IPickable>().PickUp(liftingPower)){return;}

                    UnSubscribeItem();
                    HandlePickUp(false);
                }
            }
        }
        void InsertItem()
        {
            if (!itemCanBeInserted){return;}
            if (!Input.GetKeyDown(_insertKey)){return;}
            
            if(_itemInserter.InsertItem(_interactableObject))
            {
                HandlePickUp(false);
            }
        }
        void HandlePickUp(bool pickItUp)
        {
            _objectPicker.HandlePickUp(_interactableObject);
            _pickedUpObject = pickItUp;
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

        void SubscribeToItem()
        {
            _interactableObject.GetComponent<BodyPartBase>().canInsert += CanInsert;
            _interactableObject.GetComponent<BodyPartBase>().cantInsert += CantInsert;
        }
        void UnSubscribeItem()
        {
            
        }

        void CanInsert(IItemInserter itemInserter)
        {
            _itemInserter = itemInserter;
            itemCanBeInserted = true;
        }

        void CantInsert()
        {
            itemCanBeInserted = false;
        }
    }
}
    

