using UnityEngine;

namespace MechPartStates
{
    public class DirtyState : IDamagedState
    {
        public void DamagedCables(ILocomotionContext context)
        {
     
        }

        public void Dirty(ILocomotionContext context)
        {
          
        }

        public void OutOfDateSystem(ILocomotionContext context)
        {
           
        }

        public void DamagedArmor(ILocomotionContext context)
        {
           
        }

        public void Repaired(ILocomotionContext context)
        {
            context.SetState(new RepairedState());
            Debug.Log("Part is repaired");
        }
    }
}