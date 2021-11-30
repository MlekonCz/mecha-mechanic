using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Buildings
{
    public class RepairBench : MonoBehaviour, IItemInserter
    {
        private IItemInserter _itemInserterImplementation;
        [SerializeField] private Transform repairPosition;

       
        
        

        public bool InsertItem(GameObject interactedObject)
        { 
            interactedObject.transform.position = repairPosition.position;
            interactedObject.transform.rotation = repairPosition.rotation;
            return true;
        }
    }
}