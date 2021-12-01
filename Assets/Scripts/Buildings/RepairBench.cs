using System;
using Cinemachine;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Buildings
{
    public class RepairBench : BuildingsBase
    {
        public override bool InsertItem(GameObject interactedObject)
        {
            ConfigureObject(interactedObject);
            ActivateTimeline();
            return true;
        }

        protected override void ConfigureObject(GameObject interactedObject)
        {
            base.ConfigureObject(interactedObject);
            interactedObject.GetComponent<Animator>().SetBool("isInRepairStation",true);
        }
        
        
    }
}