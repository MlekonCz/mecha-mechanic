using System;
using Buildings;
using Interactions;
using MechPartStates;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace MechParts
{
    /// <summary>
    /// Will decide later based on added features
    /// if I will change it back to normal class or keep it abstract,
    /// </summary>
   
    public abstract class BodyPartBase : MonoBehaviour, IPickable
    {
       [SerializeField] private MechPartDefinition _mechPartDefinition;
       [SerializeField] private Transform nextBuildingPosition;

        public delegate void CallBackType(IItemInserter inserter);
        public event CallBackType canInsert;
        public event Action cantInsert;
        
        public StatesForMechParts _stateSetter;
        protected virtual void Start()
        {
            _stateSetter = FindObjectOfType<StatesForMechParts>();
            SetState();
        }

        protected virtual void SetState()
        {
            _stateSetter.SetRandomState(gameObject);
        }

        public virtual void DropItem()
        {
            
        }

        protected virtual void OnTriggerEnter(Collider other)
        {
            IItemInserter component;
            if (other.gameObject.TryGetComponent(out component))
            {
                canInsert?.Invoke(component);
            }
        }

        protected virtual void OnTriggerExit(Collider other)
        {
            IItemInserter component;
            if (other.gameObject.TryGetComponent(out component))
            {
                cantInsert?.Invoke();
            }
        }

        public virtual bool PickUp(float liftingPower)
        {
            if (_mechPartDefinition._weight > liftingPower)
            {
                Debug.Log("Item is too heavy to lift");
                return false;
            }
            else
            {
                Debug.Log("You picked item up");
                return true;
            }
        }

      
    }
}
