using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Buildings
{
    public abstract class BuildingsBase : MonoBehaviour, IItemInserter
    {
        [TabGroup("Object Data")]
        [SerializeField] protected Transform repairPosition;
        [TabGroup("Object Data")]
        [SerializeField] protected GameObject cmCamera;
        [TabGroup("TimeLines")]
        [SerializeField] protected TimelineAsset enterStation;
        [TabGroup("TimeLines")]
        [SerializeField] protected TimelineAsset leaveStation;
        
        private bool stationIsBeingUsed = false;
        protected PlayableDirector _director;
        
        protected virtual void Start()
        {
            _director = GetComponent<PlayableDirector>();
        }
        
        public abstract bool InsertItem(GameObject interactedObject);
        
       protected virtual void ConfigureObject(GameObject interactedObject)
        {
            interactedObject.transform.position = repairPosition.position;
            interactedObject.transform.rotation = repairPosition.rotation;
            interactedObject.GetComponent<Rigidbody>().isKinematic = true;
            stationIsBeingUsed = true;
        }
       
       protected virtual void ActivateTimeline()
       {
           _director.playableAsset = enterStation;
           _director.Play();
           cmCamera.SetActive(true);
       }
       
        protected virtual void Update() 
        {
            if (Input.GetKeyDown(KeyCode.Q) && stationIsBeingUsed) // only for testing purpose for now
            {
                _director.playableAsset = leaveStation;
                _director.Play();
                stationIsBeingUsed = false;
            }
        }
    }
}