using UnityEngine;

namespace Core
{
    public class Singleton : MonoBehaviour
    {
        private void Awake()
        {
            int numSingletons = FindObjectsOfType<Singleton>().Length;
            if (numSingletons > 1)
            {
                Destroy(gameObject);
            }
            else
            {
                DontDestroyOnLoad(gameObject);
            }
        }
    }
}
