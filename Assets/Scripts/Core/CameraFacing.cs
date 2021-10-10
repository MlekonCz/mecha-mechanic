using UnityEngine;

namespace Core
{
    public class CameraFacing : MonoBehaviour
    { 
        void LateUpdate()
        {
            if (Camera.main is { }) transform.forward = Camera.main.transform.forward;
        }
    }
}