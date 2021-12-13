using MechPartComponents;
using UnityEngine;
using UnityEngine.Serialization;

namespace Interactions
{
    public class MouseDetect : MonoBehaviour
    {
        public Camera myCam;
        
        
       [SerializeField] private LayerMask _layerMaskToSee; 
        
        void Update()
        {
            
            GetMouseInfo();
        }
        
        void GetMouseInfo()
        {
            Ray ray = myCam.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(myCam.transform.position,Input.mousePosition,Color.red);
            RaycastHit hit;
     
            if (Physics.Raycast(ray,out hit, Mathf.Infinity,_layerMaskToSee))
            {
                if (Input.GetMouseButton(0))
                {
                    hit.collider.GetComponentInParent<Screw>().ScrewBolt();
                }
               
               
            }
            
     
        }

    }
}