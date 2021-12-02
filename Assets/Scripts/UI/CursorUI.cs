using UnityEngine;

namespace UI
{
    /// <summary>
    /// Not Refactored yet
    /// </summary>
    public class CursorUI : MonoBehaviour
    {
        [SerializeField] private GameObject basicCursor = null;
        [SerializeField] private GameObject constructionCursor = null;


        public void CanBuild(bool canBuild)
        {
            basicCursor.SetActive(!canBuild);
            constructionCursor.SetActive(canBuild);
        }

       
    }
    
}
