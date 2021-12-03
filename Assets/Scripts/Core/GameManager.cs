using System;
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
            _simpleWalkerController = FindObjectOfType<SimpleWalkerController>();
            _cameraController = FindObjectOfType<CameraController>();
        }

        public void LockMovement(bool lockMovement)
        {
            _simpleWalkerController._lockedMovement = lockMovement;
            _cameraController._lockedCamera = lockMovement;
        }
    }
}