using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace MechPartComponents
{
    public abstract class PartComponent : MonoBehaviour
    {
        
        [SerializeField] protected GameObject[] _availableModel;
        [SerializeField] protected List<GameObject> _screws = new List<GameObject>();
        [SerializeField] private int _totalNumberOfScrews;


        private bool _isOperable = true;
        private int _numberOfActiveScrews;
        
        private PartComponent _parentComponent = null;

        public event Action _componentIsRemoved;
        public delegate void CallBackType(bool canBeManipulated);
        public event CallBackType _screwsCanBeManipulated;


        
        
        private void OnEnable()
        {
            if (_parentComponent != null)
            {
                _parentComponent._componentIsRemoved += RemovedParentComponent;
            }
        }

        private void Awake()
        {
            AccessParentComponent();
        }

        private void Start()
        {
            _totalNumberOfScrews = _screws.Count;
            _numberOfActiveScrews = _totalNumberOfScrews;
           
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                InstallPart();
            }
            if (_numberOfActiveScrews == 0 && Input.GetKeyDown(KeyCode.U))
            {
                UnInstallPart();
            }
        }

        public void MakeComponentInteractable(bool isOperable)
        {
            _isOperable = isOperable;
        }
        protected virtual void AccessParentComponent()
        {
            _parentComponent = transform.parent.GetComponent<PartComponent>();
            if (_parentComponent == null)
            {
                _screwsCanBeManipulated?.Invoke(true);
            }
           
        }
        protected virtual void InstallPart()
        {
            if (!_isOperable){return;}
            
            foreach (GameObject screw in _screws)
            {
                screw.SetActive(true);
            }
            _availableModel[0].SetActive(true);
            
        }
        protected virtual void UnInstallPart()
        {
            _componentIsRemoved?.Invoke();
            _screwsCanBeManipulated?.Invoke(false);
            _availableModel[0].SetActive(false);
        }
        
        public void ChangeStatusOfScrew(bool activate)
        {
            if (activate)
            {
                if (_numberOfActiveScrews == _totalNumberOfScrews)
                {
                    if (_parentComponent != null)
                    {
                        _parentComponent.MakeComponentInteractable(true);
                    }
                    
                    return;
                }
                _numberOfActiveScrews++;
                
            }
            else
            {
                if (_parentComponent != null)
                {
                    _parentComponent.MakeComponentInteractable(false);
                }
               
                if (_numberOfActiveScrews == 0){return;}
                _numberOfActiveScrews--;
            }
        }

        private void RemovedParentComponent()
        {
            _screwsCanBeManipulated?.Invoke(true);
        }

        private void OnDisable()
        {
            if (_parentComponent != null)
            {
                _parentComponent._componentIsRemoved -= RemovedParentComponent;
            }
        }
    }
}