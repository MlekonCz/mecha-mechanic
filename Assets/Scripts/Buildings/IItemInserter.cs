using UnityEngine;

namespace Buildings
{
    public interface IItemInserter
    {
        bool InsertItem(GameObject interactedObject);
    }
}