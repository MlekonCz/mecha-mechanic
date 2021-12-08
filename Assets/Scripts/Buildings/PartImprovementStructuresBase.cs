using Sirenix.OdinInspector;
using UI;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Buildings
{
    /// <summary>
    /// Iam still working on this scripts and a lot of stuff here is not working properly yet
    /// so keep that in mind. Thanks
    /// </summary>
    public abstract class PartImprovementStructuresBase : StructuresBase, IItemInserter
    {
        [TabGroup("Object Data")]
        [SerializeField] protected Transform repairPosition;
        [TabGroup("Object Data")]
        [SerializeField] protected GameObject cmCamera;
        [TabGroup("TimeLines")]
        [SerializeField] protected TimelineAsset enterStation;
        [TabGroup("TimeLines")]
        [SerializeField] protected TimelineAsset leaveStation;
        
        protected PlayableDirector _director;

        protected override void Start()
        {
            base.Start();
            _director = GetComponent<PlayableDirector>();
        }

        public abstract bool InsertItem(GameObject interactedObject);
        
        protected override void RemoveItem()
        {
            CloseUI();
            _director.playableAsset = leaveStation;
            _director.Play();
            isInteracting = false;
        }
        protected override void CloseUI()
        {
            base.CloseUI();
            _director.playableAsset = leaveStation;
            _director.Play();
            isInteracting = false;
        }

        protected virtual void ConfigureObject(GameObject interactedObject)
        {
            interactedObject.transform.position = repairPosition.position;
            interactedObject.transform.rotation = repairPosition.rotation;
            interactedObject.GetComponent<Rigidbody>().isKinematic = true;
            isInteracting = true;
        }
       
       protected virtual void ActivateTimeline()
       {
           _director.playableAsset = enterStation;
           _director.Play();
           cmCamera.SetActive(true);
       }
       
        protected virtual void Update() 
        {
            if (Input.GetKeyDown(KeyCode.Q) && isInteracting) // only for testing purpose for now
            {
                CloseUI();
                _director.playableAsset = leaveStation;
                _director.Play();
                isInteracting = false;
            }
        }
    }
}