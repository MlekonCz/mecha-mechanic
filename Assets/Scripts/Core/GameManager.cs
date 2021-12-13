using CMF;
using UnityEngine;

namespace Core
{
    public class GameManager : MonoBehaviour
    {
        private SimpleWalkerController _simpleWalkerController;
        private CameraController _cameraController;

        private void Start()
        {
            GetReferences();
        }
        
        public void LockMovement(bool lockMovement)
        {
            if (lockMovement)
            {
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
            }

            if (!_simpleWalkerController || !_cameraController)
            {
                GetReferences();
            }
            _simpleWalkerController._lockedMovement = lockMovement;
            _cameraController._lockedCamera = lockMovement;
        }

        private void GetReferences()
        {
            _simpleWalkerController = FindObjectOfType<SimpleWalkerController>();
            _cameraController = FindObjectOfType<CameraController>();
        }
    }
}