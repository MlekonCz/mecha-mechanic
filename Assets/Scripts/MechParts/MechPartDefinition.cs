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

        
        public PartsOfMech mechPart => mechPart; 
        public AttributesOfParts attribute => attribute; 
        public ItemTier itemTier => itemTier;
        
        public float weight => weight;
        private GameObject _bodyPart;

        [Title("Shop Related")] 
        public float cost => cost;
        [SerializeField] private int partsToDeliver = 1;
        
    }
}