using UnityEngine;

namespace Core
{
    public class PersistentObjectSpawner : MonoBehaviour
    {
        [Tooltip("This prefab will only be spawned once and will persist between scenes.")]
        [SerializeField] private GameObject persistentObjectPrefab = null;

        private static bool _hasSpawned = false;

        private void Awake()
        {
            if (_hasSpawned){return;}

            SpawnPersistentObject();
            
            _hasSpawned = true;
        }

        private void SpawnPersistentObject()
        {
            GameObject persistentObject = Instantiate(persistentObjectPrefab);
            DontDestroyOnLoad(persistentObject);
        }
    }
}
