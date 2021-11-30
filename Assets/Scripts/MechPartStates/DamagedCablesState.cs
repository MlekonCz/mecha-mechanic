using MechParts;
using UnityEngine;

namespace MechPartStates
{
    public class DamagedCablesState : IDamagedState
    {
        public void DamagedCables(ILocomotionContext context)
        {
     
        }

        public void DirtyArmor(ILocomotionContext context)
        {
          
        }

        public void OutOfDateSystem(ILocomotionContext context)
        {
           
        }

        public void DamagedArmor(ILocomotionContext context)
        {
           
        }

        public void Repaired(ILocomotionContext context, GameObject user)
        {
           context.SetState(new RepairedState());
           Debug.Log("Part is repaired");
        }
    }
}