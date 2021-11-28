using UnityEngine;

namespace MechPartStates
{
    public class RepairedState : IDamagedState
    {
        public void DamagedCables(ILocomotionContext context)
        {
            context.SetState(new DamagedCablesState());
            Debug.Log("Cables are Damaged");
        }

        public void Dirty(ILocomotionContext context)
        {
            context.SetState(new DirtyState());
            Debug.Log("Part is dirty");
        }

        public void OutOfDateSystem(ILocomotionContext context)
        {
            context.SetState(new OutOfDateState());
            Debug.Log("System is out of date");
        }

        public void DamagedArmor(ILocomotionContext context)
        {
            context.SetState(new DamagedArmorState());
            Debug.Log("Armor is Damaged");
        }

        public void Repaired(ILocomotionContext context)
        {
            
        }
    }
}