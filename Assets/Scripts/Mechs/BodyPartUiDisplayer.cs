using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Mechs
{
    public class BodyPartUiDisplayer : MonoBehaviour
    {
        [SerializeField] private BodyPart bodyPart;
        [SerializeField] private GameObject informationCanvas;
        [SerializeField] private TMP_Text attributes;
        [SerializeField] private float distanceToDisplayInformation = 10f;
        private GameObject _player;
        
        private void Start()
        {
            _player = GameObject.FindGameObjectWithTag("Player");
            attributes.text = bodyPart.GetBodyPartConfig().attribute.ToString();
            
            if (transform.parent == null){return;}
            if (transform.parent.CompareTag("RobotFrame"))
            {
                Destroy(informationCanvas);
                Destroy(this);
            }
        }

        private void OnMouseOver()
        {
            if (PlayerInRange())
            {
                informationCanvas.SetActive(true);
            }
        }

        private void OnMouseExit()
        {
            informationCanvas.SetActive(false);
        }

        private bool PlayerInRange()
        {
            
            if (Vector3.Distance(transform.position, _player.transform.position) <= distanceToDisplayInformation)
            {
                return true;
            }

            return false;
        }
    }
}
