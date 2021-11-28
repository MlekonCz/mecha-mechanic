using System;
using Interactions;
using MechPartStates;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace MechParts
{
    
    public abstract class BodyPartBase : MonoBehaviour, IPickable
    {
        [FormerlySerializedAs("_bodyPartDefinition")] [SerializeField] private MechPartDefinition _mechPartDefinition;
        [SerializeField] private Transform nextBuildingPosition;
        public Transform NextBuildingPosition => nextBuildingPosition;

        public StatesForMechParts _states;

        public virtual void Start()
        {
            _states = FindObjectOfType<StatesForMechParts>();
        }

        public abstract void SetState();


        public virtual bool PickUp(float liftingPower)
        {
            if (_mechPartDefinition.weight > liftingPower)
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
