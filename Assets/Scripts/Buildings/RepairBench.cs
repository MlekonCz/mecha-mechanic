using System;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Buildings
{
    public class RepairBench : BuildingsBase
    {
        private IItemInserter _itemInserterImplementation;
        [SerializeField] private Transform repairPosition;
        [SerializeField] private GameObject cmCamera;
        private PlayableDirector _director;
        [SerializeField] private TimelineAsset enterStation;
        [SerializeField] private TimelineAsset leaveStation;
        private bool stationIsBeingUsed = false;

        private void Start()
        {
            _director = GetComponent<PlayableDirector>();
        }

        public override bool InsertItem(GameObject interactedObject)
        {
            AccessComponents(interactedObject);
            return true;
        }

        void AccessComponents(GameObject interactedObject)
        {
            _director.playableAsset = enterStation;
            _director.Play();
            cmCamera.SetActive(true);
            interactedObject.transform.position = repairPosition.position;
            interactedObject.transform.rotation = repairPosition.rotation;
            interactedObject.GetComponent<Rigidbody>().isKinematic = true;
            interactedObject.GetComponent<Animator>().SetBool("isInRepairStation",true);
            stationIsBeingUsed = true;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q) && stationIsBeingUsed)
            {
                _director.playableAsset = leaveStation;
                _director.Play();
                stationIsBeingUsed = false;
            }
        }
    }
}