using UnityEngine;

namespace UI
{
   public class WindowCloser : MonoBehaviour
   {
      [SerializeField] private GameObject objectToClose;

      private void Start()
      {
         objectToClose.SetActive(true);
         Cursor.lockState = CursorLockMode.None;
      }
      public void CloseWindow()
      {
         objectToClose.SetActive(false);
         Cursor.lockState = CursorLockMode.Locked;
      }
   }
}
