using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace MechPartStates
{
    public interface ILocomotionContext
    {
        void SetState(IDamagedState newState);
    }
    public interface IDamagedState
    {
        void DamagedCables(ILocomotionContext context);
        void DirtyArmor(ILocomotionContext context);
        void OutOfDateSystem(ILocomotionContext context);
        void DamagedArmor(ILocomotionContext context);
        void Repaired(ILocomotionContext context);
    }
    public class StatesForMechParts : MonoBehaviour, ILocomotionContext
    {
        private IDamagedState currentState = new RepairedState();
        private readonly List<Action> stateList = new List<Action>();
        
        private Action[] stateArray;

        private void Start()
        {
            stateList.Add(DamageCables);
            stateList.Add(MakePartDirty);
            stateList.Add(OutDateSystem);
            stateList.Add(DamageArmor);
            
            stateArray = stateList.ToArray();
        }

        public void SetRandomState()
        {
            stateArray[Random.Range(0, stateArray.Length)].Invoke();
        }

        public void DamageCables() => currentState.DamagedCables(this);
    
        public void MakePartDirty() => currentState.DirtyArmor(this);

        public void OutDateSystem() => currentState.OutOfDateSystem(this);
    
        public void DamageArmor() => currentState.DamagedArmor(this);
        
        public void Repair() => currentState.Repaired(this);
        void ILocomotionContext.SetState(IDamagedState newState)
        {
            currentState = newState;
        }
    }
}