using UnityEngine;

namespace Buildings
{
    public class PartContainerStructures : StructuresBase, IItemInserter
    {
        [SerializeField] private int _numberOfSlots = 1;

        public bool InsertItem(GameObject interactedObject)
        {
            //later add logic for storing items
            return false;
        }
    }
}