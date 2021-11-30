using UnityEngine;

namespace Buildings
{
    public abstract class BuildingsBase : MonoBehaviour, IItemInserter
    {


        public abstract bool InsertItem(GameObject interactedObject);
    }
}