using System;
using System.Collections.Generic;
using UnityEngine;

namespace MechPartComponents
{
    public abstract class PartComponent : MonoBehaviour
    {
        [SerializeField] protected GameObject _model;
        [SerializeField] protected List<GameObject> _screws = new List<GameObject>();
        [SerializeField] private int _totalNumberOfScrews;
        
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
            foreach (GameObject screw in _screws)
            {
                screw.SetActive(true);
            }
            _model.SetActive(true);
            
        }
        protected virtual void UnInstallPart()
        {
            _componentIsRemoved?.Invoke();
            _screwsCanBeManipulated?.Invoke(false);
            _model.SetActive(false);
        }
        
        public void ChangeStatusOfScrew(bool activate)
        {
            if (activate)
            {
                if (_numberOfActiveScrews == _totalNumberOfScrews){return;}
                _numberOfActiveScrews++;
            }
            else
            {
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