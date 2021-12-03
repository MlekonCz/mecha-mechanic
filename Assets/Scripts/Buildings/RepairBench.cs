using UnityEngine;

namespace Buildings
{
    public class RepairBench : PartImprovementStructuresBase
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