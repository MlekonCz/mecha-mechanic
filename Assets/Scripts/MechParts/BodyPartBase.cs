using System;
using Interactions;
using MechPartStates;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MechParts
{
    
    public abstract class BodyPartBase : MonoBehaviour, IPickable
    {
        [SerializeField] private BodyPartDefinition _bodyPartDefinition;
        [SerializeField] private Transform nextBuildingPosition;
        public Transform NextBuildingPosition => nextBuildingPosition;

        private StatesForMechParts _states;

        private void Start()
        {
            _states = FindObjectOfType<StatesForMechParts>();
            SetState();
        }

        public virtual void SetState()
        {
            _states.DamageCables();
        }


        public virtual bool PickUp(float liftingPower)
        {
            if (_bodyPartDefinition.weight > liftingPower)
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
