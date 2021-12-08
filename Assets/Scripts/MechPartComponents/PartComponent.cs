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
            _model.SetActive(false);
        }
    }
}