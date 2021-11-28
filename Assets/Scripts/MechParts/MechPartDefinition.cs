using MechParts;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace MechParts
{
    public enum ItemTier
    {
        T1,
        T2,
        T3,
    }
    [CreateAssetMenu(fileName = "BodyPartDefinition", menuName = "Definition/Make New Body Part", order = 0)]
    public class MechPartDefinition : ScriptableObject
    {
        [SerializeField] private GameObject bodyPrefab = null; 
        [SerializeField] public PartsOfMech mechPart;
        [SerializeField] public AttributesOfParts attribute;
        [SerializeField] public ItemTier itemTier;
        [SerializeField] public float weight = 5f;
        private GameObject _bodyPart;

        [Title("Shop Related")] 
        [SerializeField] public float cost;
        [SerializeField] private int partsToDeliver = 1;
        
        

        
        
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