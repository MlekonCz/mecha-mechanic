using Interactions;
using UnityEngine;

namespace Buildings
{
    public abstract class StructuresBase : MonoBehaviour, IInteractable
    {
        public void Interact()
        {
            // later opens UI for structure
        }
    }
}