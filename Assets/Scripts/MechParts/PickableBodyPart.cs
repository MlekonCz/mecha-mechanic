using UI;
using UnityEngine;
using UnityEngine.Serialization;

namespace MechParts
{
    /// <summary>
    /// Not Refactored yet
    /// </summary>
    public class PickableBodyPart : MonoBehaviour
    {
        // public KeyCode _interactionButton = KeyCode.E;
        // private Rigidbody _myRigidbody;
        // private float numberOfParts;
        // private bool _canBuild = false;
        // private bool active = false;
        // private bool _installPart = false;
        //
        //
        // [SerializeField] private PartsOfMech bodyPart;
        // [FormerlySerializedAs("_bodyPartDefinition")] [SerializeField] private MechPartDefinition _mechPartDefinition;
        //
        // private void Start()
        // {
        //     _myRigidbody = GetComponent<Rigidbody>();
        //     if (transform.parent == null)
        //     {
        //         return;
        //     }
        //     if (transform.parent.CompareTag("RobotFrame"))
        //     {
        //         _myRigidbody.isKinematic = true;
        //         Destroy(this);
        //     }
        // }
        //
        // public void ActivateObject()
        // {
        //     if (!active)
        //     {
        //         active = true;
        //     }
        //     else
        //     {
        //         Invoke(nameof(SetNonActive),0.2f);
        //     }
        //
        //     
        // }
        // void SetNonActive()
        // {
        //     active = false;
        // }
        // private void Update()
        // {
        //     
        //     if (!active){return;}
        //     if (!_canBuild){return;}
        //     if (!Input.GetKeyDown(_interactionButton)){return;}
        //     _installPart = true;
        // }
        //
        // private void OnTriggerStay(Collider other)
        // {
        //     if (!other.gameObject.CompareTag("RobotFrame")){return;}
        //     
        //     // foreach (INextPart part in other.GetComponents<INextPart>())
        //     // {
        //     //     if (part.GetNextBodyPart() == bodyPart)
        //     //     {
        //     //         _canBuild = true;
        //     //     }
        //     // }
        //     if (!_canBuild){return;}
        //     
        //     FindObjectOfType<CursorUI>().CanBuild(true);
        //     
        //     if (!_installPart){return;}
        //     
        //     FindObjectOfType<CursorUI>().CanBuild(false);
        //     
        //     // var mechFrame = other.gameObject.GetComponent<MechFrameBuilder>();
        //     // mechFrame.InstallBodyPart(bodyPartConfig);
        //     Destroy(gameObject);
        //     
        // }
        //
        // private void OnTriggerExit(Collider other)
        // {
        //     if (!other.gameObject.CompareTag("RobotFrame")){return;}
        //
        //     _canBuild = false;
        //     FindObjectOfType<CursorUI>().CanBuild(false);
        // }
    }
}
