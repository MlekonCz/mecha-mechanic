using UnityEngine;
using UnityEngine.Serialization;

namespace Mechs
{
    [CreateAssetMenu(fileName = "BodyPartConfig", menuName = "Body Parts/Make New Body Part", order = 0)]
    public class BodyPartConfig : ScriptableObject
    {
        [SerializeField] private GameObject bodyPrefab = null; 
        [Tooltip("only if this is arm")] [SerializeField] private GameObject rightArmPrefab;
        [SerializeField] public PartsOfMech mechPart;
        [SerializeField] public AttributesOfParts attribute;
        [SerializeField] public bool isArm = false;
        private GameObject _bodyPart;

        [Header("Shop Related")] 
        [SerializeField] public float cost;
        [SerializeField] private int partsToDeliver = 1;
        
        
        public void Spawn(Transform buildPoint, GameObject parent, bool isRight)
        {
            if (bodyPrefab ==null){return;}

            if (isRight)
            {
                SpawningRightArm(buildPoint, parent);
            }
            else
            {
                _bodyPart = Instantiate(bodyPrefab, buildPoint.position, Quaternion.identity);
                _bodyPart.transform.parent = parent.transform;
            }
            
        }
        
        public Transform GetNextBuildPosition()
        {
            if (_bodyPart.TryGetComponent(out BodyPart bodyPart))
            {
                return bodyPart.NextBuildingPosition;
            }
            return null;
        }
        
        
        private void SpawningRightArm(Transform buildPoint, GameObject parent)
        {
            if (rightArmPrefab == null)
            {
                Debug.LogError("Didnt put prefab for right arm");
            }

            _bodyPart = Instantiate(rightArmPrefab, buildPoint.position, Quaternion.identity);
            _bodyPart.transform.parent = parent.transform;

            var position = buildPoint.position;
            Vector3 bodyPosition = new Vector3(Mathf.Abs(position.x), position.y, position.z);
            _bodyPart.transform.position = bodyPosition;
        }

        public void DeliverPart(Transform deliveryLocation)
        { 
            if (partsToDeliver == 1)
            {
                _bodyPart = Instantiate(bodyPrefab, deliveryLocation.position, Quaternion.identity);
            }
            else
            {
                for (int i = 0; i < partsToDeliver; i++)
                {
                    _bodyPart = Instantiate(bodyPrefab, deliveryLocation.position, Quaternion.identity);
                }
            }
        }
        
    }
}