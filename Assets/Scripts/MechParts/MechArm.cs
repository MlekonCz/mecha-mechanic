

namespace MechParts
{
    public class MechArm : BodyPartBase
    {
        public override void Start()
        {
            base.Start();
            SetState();
        }

        public override void SetState()
        {
            _states.DamageCables();
        }
    }
}