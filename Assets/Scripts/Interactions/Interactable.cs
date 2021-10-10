using Currency;
using UnityEngine;

namespace Interactions
{
    public class Interactable : MonoBehaviour
    {
        [SerializeField] private Computer computer = null;
        
        public void InteractWithObject()
        {
            if (computer != null)
            {
             computer.AccessComputer();   
            }
        }
    }
}
