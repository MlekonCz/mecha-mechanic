

using UnityEngine;

namespace MechParts
{
    public class MechArm : BodyPartBase
    {
        public override string GetPartName()
        {
            return gameObject.name;
        }
    }
}