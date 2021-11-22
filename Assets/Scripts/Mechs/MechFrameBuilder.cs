using System;
using System.Collections.Generic;
using UnityEngine;

namespace Mechs
{
    public class MechFrameBuilder : MonoBehaviour, INextPart
    {
        [SerializeField] private List<GameObject> bodyParts;

        [SerializeField] private Material transparentMaterial;
        [SerializeField] private Transform buildPoint = null;
        private Transform _originalBuildPoint = null;
        [SerializeField] public GameObject workOnMech = null;
        
        [Header("testing")]
        [SerializeField] private bool testButton = false;
        [SerializeField] private BodyPartConfig testPart = null;
        

        private bool _legsPlaced = false;
        private bool _torsoPlaced = false;
        private bool _startedConstruction = false;
            
        public event Action onConstructionStarted;
        public event Action onConstructionFinished;
        
        private void Start()
        {
            _originalBuildPoint = buildPoint;
            foreach (GameObject part in bodyParts)
            {
                part.GetComponent<Renderer>().material = transparentMaterial;
            }
        }
        private void Update()
        {
            if (_startedConstruction)
            {
                ResetMechFrameConstruction();
            }

            
            if (testButton == true)
            {
                InstallBodyPart(testPart);
                testButton = false;
            }
        }

        public void InstallBodyPart(BodyPartConfig bodyPart)
        {
            foreach (GameObject part in bodyParts)
            {
                if (ConditionCheck(bodyPart, part)) continue;
                onConstructionStarted?.Invoke();
                if (bodyPart.isArm) // if you spawn arm
                {
                    if (InstallArm(bodyPart, part)){return;}
                    else continue;
                }
                bodyPart.Spawn(buildPoint, workOnMech, false); // if you spawn legs or torso 
                if (bodyPart.GetNextBuildPosition() == null) // if you spawn arm
                {
                    part.SetActive(false);
                    return; 
                }
                _startedConstruction = true;
                buildPoint = bodyPart.GetNextBuildPosition();
                part.SetActive(false);
                return;
            }
        }

        private bool ConditionCheck(BodyPartConfig bodyPart, GameObject part)
        {
            if (part.activeInHierarchy == false){return true;}
            var partConfig = part.GetComponent<BodyPart>();
            
            if (partConfig.GetBodyPartConfig() == null) {return true;}
            if (partConfig.GetBodyPartConfig().mechPart != bodyPart.mechPart){return true;}

            return false;
        }
        private bool InstallArm(BodyPartConfig bodyPart, GameObject part)
        {
            int numOfPlacedParts = workOnMech.transform.childCount;
            if (numOfPlacedParts == 2)
            {
                part.SetActive(false);
                bodyPart.Spawn(buildPoint, workOnMech, false);
                return true;
            }

            if (numOfPlacedParts == 3)
            {
                part.SetActive(false);
                bodyPart.Spawn(buildPoint, workOnMech, true);
                return true;
            }

            return false;
        }
        private void ResetMechFrameConstruction()
        {
            if (workOnMech.transform.childCount != 0){return;}
            foreach (GameObject part in bodyParts)
            {
                part.SetActive(true);
            }
            _startedConstruction = false;
            buildPoint = _originalBuildPoint;
            onConstructionFinished?.Invoke();
        }

        public PartsOfMech? GetNextBodyPart()
        {
            int numOfPlacedParts = workOnMech.transform.childCount;
            
            if (numOfPlacedParts ==0)
            {
                return PartsOfMech.Leg;
            }
            else if (numOfPlacedParts == 1 )
            {
                return PartsOfMech.Torso;
            }
            
            else if (numOfPlacedParts <= 3)
            {
                return PartsOfMech.Arms;
            }

            return null;
        }
    }
}
