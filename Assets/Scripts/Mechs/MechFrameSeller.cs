using System.Collections.Generic;
using UnityEngine;

namespace Mechs
{
    public class MechFrameSeller : MonoBehaviour
    {
        [SerializeField] public GameObject workOnMech = null;
        private List<AttributesOfParts> _mechAttributes = null;

        
        
        public List<AttributesOfParts> SellMech()
        {
            if (workOnMech.transform.childCount != 4){return null;}
            
            Invoke(nameof(DestroySoldItems),0.2f);
            return GetMechAttributes();
        }
        
        public List<AttributesOfParts> GetMechAttributes()
        {
            _mechAttributes = null;
            foreach (Transform child in workOnMech.transform)
            {
                if (_mechAttributes == null)
                {
                    _mechAttributes = new List<AttributesOfParts>();
                }
                _mechAttributes.Add(child.gameObject.GetComponent<BodyPart>().GetBodyPartConfig().attribute);
            }
            return _mechAttributes;
        }

        void DestroySoldItems()
        {
            foreach (Transform child in workOnMech.transform)
            {
                GameObject.Destroy(child.gameObject);
            }
        }
        
        
    }
}