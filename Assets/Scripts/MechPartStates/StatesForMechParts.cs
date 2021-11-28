using UnityEngine;

namespace MechPartStates
{
    public interface ILocomotionContext
    {
        void SetState(IDamagedState newState);
    }
    public interface IDamagedState
    {
        void DamagedCables(ILocomotionContext context);
        void Dirty(ILocomotionContext context);
        void OutOfDateSystem(ILocomotionContext context);
        void DamagedArmor(ILocomotionContext context);
        void Repaired(ILocomotionContext context);
    }
    public class StatesForMechParts : MonoBehaviour, ILocomotionContext
    {
        private IDamagedState currentState = new RepairedState();
        
        public void DamageCables() => currentState.DamagedCables(this);
    
        public void MakePartDirty() => currentState.Dirty(this);

        public void OutDateSystem() => currentState.OutOfDateSystem(this);
    
        public void DamageArmor() => currentState.DamagedArmor(this);
        
        public void Repair() => currentState.Repaired(this);
        void ILocomotionContext.SetState(IDamagedState newState)
        {
            currentState = newState;
        }
    }
}