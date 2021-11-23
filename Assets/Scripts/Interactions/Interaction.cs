using System;
using System.Linq;
using Customers;
using Impact.Demo;
using Mechs;
using UnityEngine;

namespace Interactions
{
    public class Interaction : MonoBehaviour
    {
        [SerializeField] private LayerMask layerMaskToIgnore;
        
        Ray ray;
        RaycastHit hit;
        [Header("Camera")]
        [SerializeField] Transform mainCamera;

        [Header("Interaction Properties")]
        [SerializeField] float interactionDistance;
        
        [Header("Pick Up Objects")]
        [SerializeField]
        private float pickedUpDrag = 20f;
        [SerializeField]
        private float pickupHoldForce = 20f;
        
        private Rigidbody pickedUpObject;
        private float pickupDistance;

        readonly string[] _interactionTags = {"Interactable", "Pickable", "Customer"};
        
        [SerializeField] private GameObject interactableObject;

        [Header("Interact Button")]
        [SerializeField] KeyCode interactionButton;

        [Header("Booleans")]
        private bool itemIsBeingHeld = false;
        private bool _itIsMechPart = false;
        
        private void Update()
        {
            StartInteraction();
        }
        private void FixedUpdate()
        {
            UpdatePickedUpObject();
        }

        #region PickUpInteraction

        private void StartInteraction()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (pickedUpObject == null)
                {
                    if (!CheckingForInteractableObject()){return;}
                    if (!pickupObject())
                    {
                        PressButton(false);
                    }
                }
                else DropObject(0);
            }

            if (Input.GetKey(KeyCode.E) && pickedUpObject == null)
            {
                PressButton(true);
            }
        }

        private bool CheckingForInteractableObject() // control if it is pickable or customer or interactable
        {
            if (pickedUpObject != null){return true;}
            
            RaycastHit hit; 
            if (Physics.Raycast(mainCamera.position + mainCamera.forward, mainCamera.forward,
                out hit, interactionDistance, ~layerMaskToIgnore))
            {
                if (!_interactionTags.Contains(hit.transform.tag)){return false;}
                interactableObject = hit.transform.gameObject;
                if (!hit.transform.CompareTag("Pickable"))
                {
                    Interact();
                    return false;
                }

                if (hit.transform.CompareTag("Pickable"))
                {
                    return true;
                }
                
            }
            return false;
        }
        private bool pickupObject()
        {
            RaycastHit hit;
            if (Physics.Raycast(mainCamera.position + mainCamera.forward, mainCamera.forward, out hit, interactionDistance))
            {
                if (!hit.transform.CompareTag("Pickable"))
                {
                    return false;
                }
                if (hit.rigidbody != null)
                {
                    pickupDistance = Mathf.Max(3, hit.distance);
                    pickedUpObject = hit.rigidbody;
                    pickedUpObject.useGravity = false;
                    pickedUpObject.drag = pickedUpDrag;
                    pickedUpObject.angularDrag = pickedUpDrag;

                   
                    pickedUpObject.GetComponent<PickableBodyPart>().ActivateObject(); //change later with more Pickables

                    return true;
                }
            }

            return false;
        }
        
        private void UpdatePickedUpObject()
        {
            if (pickedUpObject != null)
            {
                Vector3 target = mainCamera.position + mainCamera.forward * pickupDistance;
                Vector3 dir = target - pickedUpObject.position;

                pickedUpObject.AddForce(dir * pickupHoldForce, ForceMode.VelocityChange);
            }
        }
        private void DropObject(float force)
        {
            if (pickedUpObject != null)
            {
                pickedUpObject.GetComponent<PickableBodyPart>().ActivateObject(); //change later with more Pickables

                pickedUpObject.useGravity = true;
                pickedUpObject.drag = 0;
                pickedUpObject.angularDrag = 0.05f;
                pickedUpObject.AddForce(mainCamera.forward * force);
                pickedUpObject = null;
            }
        }
        private bool PressButton(bool hold) // I will use this later when I will learn how to use impact asset pack - now it does nothing
        {
            RaycastHit hit;
            if (Physics.Raycast(mainCamera.position + mainCamera.forward, mainCamera.forward, out hit, interactionDistance))
            {
                DemoButton button = hit.collider.GetComponentInParent<DemoButton>();

                if (button != null)
                {
                    if (hold)
                        button.Hold();
                    else
                        button.Press();
                }
            }
            return false;
        }
        

        #endregion
        
        #region CustomerAndItemInteraction
        
        private void Interact() // final control of which item is interacted with
        {
            if (interactableObject == null){return;}
        
            if (interactableObject.CompareTag("Interactable"))
            {
                interactableObject.GetComponent<IInteractable>().Use();
            }
            else if (interactableObject.CompareTag("Customer"))
            {
                interactableObject.GetComponent<Customer>().InteractWithObject();
            }
        }

        #endregion
    }
}
    

