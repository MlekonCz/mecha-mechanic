using UnityEngine;

namespace MechPartStates
{
    public class DamagedArmorState : IDamagedState
    {
        public void DamagedCables(ILocomotionContext context, GameObject item)
        {
     
        }

        public void DirtyArmor(ILocomotionContext context, GameObject item)
        {
          
        }

        public void OutOfDateSystem(ILocomotionContext context, GameObject item)
        {
           
        }

        public void DamagedArmor(ILocomotionContext context, GameObject item)
        {
           
        }

        public void Repaired(ILocomotionContext context, GameObject item)
        {
            context.SetState(new RepairedState(), item);
            Debug.Log("Part is repaired");
        }
    }
}