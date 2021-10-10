using System;
using UnityEngine;
using UnityEngine.UI;

namespace Currency
{
    public class Computer : MonoBehaviour
    {
        [SerializeField] private GameObject computerScreen = null;
        [SerializeField] private KeyCode keyCodeToLeave = KeyCode.Q;
        private Color orange;

        [Header("Tabs")] 
        [SerializeField] private GameObject MainScreen;
        [SerializeField] private GameObject ShopScreen;

        [Header("Booleans")]
        private bool isUsed = false;
        private bool isActiveWindow = false;

        private void Start()
        {
            computerScreen.SetActive(false);
        }

        private void Update()
        {
            if (!isUsed){return;}
            if (Input.GetKeyDown(keyCodeToLeave))
            {
                CloseComputer();
            }
        }
        public void CloseComputer()
        {
            computerScreen.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            isUsed = false;
        }
        public void AccessComputer()
        {
            isUsed = true;
            computerScreen.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
        }
        

    }
}
