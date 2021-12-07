using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace MechPartStates
{
    public interface ILocomotionContext
    {
        void SetState(IDamagedState newState, GameObject item);
    }
    public interface IDamagedState
    {
        void DamagedCables(ILocomotionContext context,GameObject item);
        void DirtyArmor(ILocomotionContext context,GameObject item);
        void OutOfDateSystem(ILocomotionContext context,GameObject item);
        void DamagedArmor(ILocomotionContext context,GameObject item);
        void Repaired(ILocomotionContext context,GameObject item);
    }
    public class StatesForMechParts : MonoBehaviour, ILocomotionContext
    {
        private IDamagedState currentState = new RepairedState();
        private readonly List<Action> stateList = new List<Action>();
        private GameObject _item;
        
        private Action[] stateArray;

        private void Start()
        {
         UpdateStateList();  
        }

        public string GetCurrentState()
        {
            switch (currentState)
            {
                case DamagedArmorState _damagedArmorState:
                    return "Armor is damaged";
                break;
                case DamagedCablesState _damagedCables:
                    return "Cables are damaged";
                break;
                case DirtyState _dirtyState:
                    return "Part is dirty";
                break;
                case OutOfDateState _outOfDateState:
                    return "System is out of date";
                break;
                default:
                    return "Part is repaired";
            }

        }
        
       private void UpdateStateList()
        {
            stateList.Add(DamageCables);
            stateList.Add(MakePartDirty);
            stateList.Add(OutDateSystem);
            stateList.Add(DamageArmor);
            
            stateArray = stateList.ToArray();
        }

        public void SetRandomState(GameObject item)
        {
            _item = item;
            stateArray[Random.Range(0, stateArray.Length)].Invoke();
        }

        public void DamageCables() => currentState.DamagedCables(this, _item);
    
        public void MakePartDirty() => currentState.DirtyArmor(this, _item);

        public void OutDateSystem() => currentState.OutOfDateSystem(this, _item);
    
        public void DamageArmor() => currentState.DamagedArmor(this, _item);
        
        public void Repair() => currentState.Repaired(this, _item);
        
        void ILocomotionContext.SetState(IDamagedState newState, GameObject item)
        {
            _item = item;
            currentState = newState;
        }

    }
}