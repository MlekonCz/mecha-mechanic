using System;
using Currency;
using UnityEngine;

namespace Interactions
{
    public class Interactable : MonoBehaviour
    {
        [SerializeField] private Computer computer = null;

        private void Start()
        {
            computer = FindObjectOfType<Computer>();
        }

        public void InteractWithObject()
        {
            if (computer != null)
            {
             computer.AccessComputer();   
            }
        }
    }
}
