using MechParts;
using UnityEngine;

namespace MechPartStates
{
    public class RepairedState : IDamagedState
    {
        private static readonly int _color = Shader.PropertyToID("_Color");

        public void DamagedCables(ILocomotionContext context, GameObject item)
        {
            item.GetComponentInChildren<Renderer>().material.SetColor(_color,Color.cyan);
            context.SetState(new DamagedCablesState(),item);
            Debug.Log("Cables are Damaged");
        }

        public void DirtyArmor(ILocomotionContext context, GameObject item)
        {
            item.GetComponentInChildren<Renderer>().material.SetColor(_color,Color.yellow);
            context.SetState(new DirtyState(),item);
            Debug.Log("Part is dirty");
        }

        public void OutOfDateSystem(ILocomotionContext context, GameObject item)
        {
            item.GetComponentInChildren<Renderer>().material.SetColor(_color,Color.red);
            context.SetState(new OutOfDateState(),item);
            Debug.Log("System is out of date");
        }

        public void DamagedArmor(ILocomotionContext context, GameObject item)
        {
            item.GetComponentInChildren<Renderer>().material.SetColor(_color,Color.black);
            context.SetState(new DamagedArmorState(),item);
            Debug.Log("Armor is Damaged");
        }

        public void Repaired(ILocomotionContext context, GameObject item)
        {
        }
    }
}