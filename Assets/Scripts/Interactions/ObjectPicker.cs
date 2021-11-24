using Mechs;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace Interactions
{
    public class ObjectPicker : MonoBehaviour
    {
        [Title("Camera")]
        [SerializeField] Transform mainCamera;

        [Title("Interaction")]
        [SerializeField] private float _interactionDistance = 2.5f;
        [SerializeField] private float _pickedUpDrag = 10f;
        [SerializeField] private float _pickupHoldForce = 20f;

        
        private Rigidbody _pickedUpObject;
        private bool _itemIsBeingHeld = false;
        
        
        public void HandlePickUp(GameObject pickedObject)
        {
            if (_itemIsBeingHeld == true)
            {
                DropObject(0);
                _itemIsBeingHeld = false;
                return;
            }

            _itemIsBeingHeld = true;
            _pickedUpObject = pickedObject.GetComponent<Rigidbody>();
            PickupObject();
        }
        private void FixedUpdate()
        {
            UpdatePickedUpObject();
        }
        
        private void PickupObject()
        {
            _pickedUpObject.useGravity = false; 
            _pickedUpObject.drag = _pickedUpDrag; 
            _pickedUpObject.angularDrag = _pickedUpDrag;

            _pickedUpObject.GetComponent<PickableBodyPart>().ActivateObject();
        }
        
        private void UpdatePickedUpObject()
        {
            if (_pickedUpObject != null)
            {
                Vector3 target = mainCamera.position + mainCamera.forward * _interactionDistance;
                Vector3 dir = target - _pickedUpObject.position;
                

                _pickedUpObject.AddForce(dir * _pickupHoldForce, ForceMode.VelocityChange);
            }
        }
        private void DropObject(float force)
        {
            if (_pickedUpObject != null)
            {
                _pickedUpObject.GetComponent<PickableBodyPart>().ActivateObject(); //change later with more Pickables

                _pickedUpObject.useGravity = true;
                _pickedUpObject.drag = 0;
                _pickedUpObject.angularDrag = 0.05f;
                _pickedUpObject.AddForce(mainCamera.forward * force);
                _pickedUpObject = null;
            }
        }
    }
}