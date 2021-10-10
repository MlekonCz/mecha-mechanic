using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Customers
{
    public class CustomerSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject customerPrefab;
        [SerializeField] private List<ShoppingPath> customerPaths;
        [SerializeField] private List<GameObject> customerDialogues; // add later dialogues
        [SerializeField] private float timeBetweenCustomersUpperLimit = 15;
        [SerializeField] private float timeBetweenCustomersBottomLimit = 30;
        [SerializeField] private Transform spawnLocation;
        private float _timeToSpawnNextCustomer;
        private float _timeSinceLastSpawn;

        private void Update()
        {
            if (customerPaths.Count == 0 ){return;}
            if (Time.time > _timeToSpawnNextCustomer + _timeSinceLastSpawn)
            {
                _timeToSpawnNextCustomer = Random.Range(timeBetweenCustomersBottomLimit, timeBetweenCustomersUpperLimit);
                GameObject instance = Instantiate(customerPrefab, spawnLocation);
                instance.transform.parent = transform;
                Debug.Log("Spawn Customer");
                _timeSinceLastSpawn = Time.time;
            }
        }


        void SpawnCustomer()
        {
           
            
        }

        public ShoppingPath GetPath()
        {
            int maxNumber = customerPaths.Count;
            int randomNumber = Random.Range(0, maxNumber);

            StartCoroutine(DeleteUsedPath(randomNumber));
            
            return customerPaths[randomNumber];
        }
        
        public void ReturnPath(ShoppingPath path)
        {
            customerPaths.Add(path);
        }

        IEnumerator DeleteUsedPath(int number)
        {
            yield return new WaitForSeconds(1f);
            customerPaths.Remove(customerPaths[number]);
        }
       
    }
}
