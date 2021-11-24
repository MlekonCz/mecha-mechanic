using System;
using System.Collections.Generic;
using Currency;
using Mechs;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace Customers
{
    public class Customer : MonoBehaviour
    {
        
        /// <summary>
        /// Change this script later into separate scripts each for 1 purpose
        /// </summary>
        
        
        [Header("Movement")]
        [SerializeField] private ShoppingPath shoppingPath;
        [SerializeField] private float speed = 5f;
        [SerializeField] private float waypointTolerance = 1f;
        [SerializeField] private float dwellingTime = 1f;
        private CustomerSpawner _spawner;
        private NavMeshAgent _navMeshAgent;
        private float _timeSinceArrivingAtWaypoint = Mathf.Infinity;
        private int _currentWaypointIndex = 0;
        
        [Header("Shopping")]
        [SerializeField] private CustomerCanvas customerCanvas;
        [SerializeField] private float bottomRangeForSellMoney = 50f;
        [SerializeField] private float upperRangeForSellMoney = 150f;
        [SerializeField] [Tooltip("percentage")] 
        private float reducedMoneyPerMissingAttribute = 20f;
        [SerializeField] private List<AttributesOfParts> _availableAttributes = new List<AttributesOfParts>();
        [SerializeField] private List<AttributesOfParts> requiredAttributes = new List<AttributesOfParts>();
        private List<String> _namesOfAttributes;
        private float _moneyForMech;
        private bool _isBuying = false;
        private int _numberOfRequiredAttributes;
        private int _matchingAttributes;
        
        
        private void Awake()
        {
            _spawner = GetComponentInParent<CustomerSpawner>();
            shoppingPath = _spawner.GetPath();
        }

        private void Start()
        {
            _moneyForMech = Random.Range(bottomRangeForSellMoney, upperRangeForSellMoney);
            _navMeshAgent = GetComponent<NavMeshAgent>();
            GetRequiredAttributes();
            customerCanvas.SetCustomerCanvas(ListOfParts(), _moneyForMech);
        }
        private void Update()
        {
           
            _timeSinceArrivingAtWaypoint += Time.deltaTime;
            WalkingBehaviour();
        }

        #region Walking

        private void WalkingBehaviour()
        {
            if (_isBuying){return;}
            Vector3 nextPosition = transform.position;
            if (shoppingPath != null)
            {
                if (AtWaypoint())
                {
                    _timeSinceArrivingAtWaypoint = 0;
                    CycleWaypoint();
                }
                nextPosition = GetCurrentWaypoint();
            }
            if (_timeSinceArrivingAtWaypoint > dwellingTime)
            {
                MoveTo(nextPosition);
            }
        }

        private void MoveTo(Vector3 destination)
        {
            _navMeshAgent.destination = destination;
            _navMeshAgent.speed = speed;
        }
        private bool AtWaypoint()
        {
            float distanceToWaypoint = Vector3.Distance(transform.position, GetCurrentWaypoint());
            return distanceToWaypoint < waypointTolerance;
        }
       
        private void CycleWaypoint()
        {
            if (shoppingPath.GetNextIndex(_currentWaypointIndex) == 0)
            {
                _spawner.ReturnPath(shoppingPath);
                Destroy(gameObject);
            }
            _currentWaypointIndex = shoppingPath.GetNextIndex(_currentWaypointIndex);
        }
        
        private Vector3 GetCurrentWaypoint()
        {
            return shoppingPath.GetWaypoint(_currentWaypointIndex);
        }
        
        #endregion

        #region Shopping
        
        void GetRequiredAttributes()
        {
            _numberOfRequiredAttributes = Random.Range(0, 3);
            foreach (AttributesOfParts attribute in Enum.GetValues(typeof(AttributesOfParts)))
            {
                _availableAttributes.Add(attribute);
            }
            
            for (int i = 0; i < _numberOfRequiredAttributes; i++)
            {
                int randomPart = Random.Range(0, _availableAttributes.Count - 1);
                
                requiredAttributes.Add(_availableAttributes[randomPart]);
                _availableAttributes.Remove(_availableAttributes[randomPart]);
            }
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Counter"))
            {
                _isBuying = true;
            }
        }

        public void StopShopping()
        {
            _isBuying = false;
        }

        public float GetRewardMoney()
        {
            return _moneyForMech;
        }
        public bool SellMech()
        {
            if (FindObjectOfType<MechFrameSeller>().SellMech() == null)
            {
                Debug.Log("false");
                return false;
            }
            Debug.Log("true");
            var percentageToReduce = CalculatePenalty();

            FindObjectOfType<PlayerMoney>().SetMoney(_moneyForMech * percentageToReduce);
            return true;
        }
        private float CalculatePenalty()
        {
            foreach (AttributesOfParts requiredAttribute in requiredAttributes)
            {
                foreach (AttributesOfParts attribute in FindObjectOfType<MechFrameSeller>().SellMech())
                {
                    if (attribute != requiredAttribute)
                    {
                        continue;
                    }

                    _matchingAttributes++;
                    break;
                }
            }
            int missingAttributes = _numberOfRequiredAttributes - _matchingAttributes;
            float percentageToReduce = (100f - missingAttributes * reducedMoneyPerMissingAttribute) / 100;
            return percentageToReduce;
        }

        private List<String> ListOfParts()
        {
            _namesOfAttributes = new List<string>();
            foreach (AttributesOfParts attribute in requiredAttributes)
            {
                _namesOfAttributes.Add(attribute.ToString());
            }
            return _namesOfAttributes;
        }
        
        public void InteractWithObject()
        {
            Debug.Log("Interacting with customer");
            customerCanvas.OpenWindow(_moneyForMech);
        }
        

        #endregion

    }
}
