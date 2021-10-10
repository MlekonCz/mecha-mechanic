using System;
using UnityEngine;

namespace Mechs
{
    public class BodyPart : MonoBehaviour
    {
        [SerializeField] private BodyPartConfig _bodyPartConfig;
        [SerializeField] private Transform nextBuildingPosition;

        private void Start()
        {
            if (transform.parent == null){return;}
            if (transform.parent.CompareTag("RobotFrame"))
            {
                gameObject.tag = "Untagged";
            }
        }

        public BodyPartConfig GetBodyPartConfig()
        {
            return _bodyPartConfig;
        }

        public Transform GetNextBuildingPosition()
        {
            return nextBuildingPosition;
        }
    }
}
