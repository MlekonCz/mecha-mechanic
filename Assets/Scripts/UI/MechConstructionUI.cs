using System;
using Mechs;
using TMPro;
using UnityEngine;

namespace UI
{
    public class MechConstructionUI : MonoBehaviour
    {
        private MechFrameSeller _mechFrame;
        [SerializeField] private GameObject constructionCanvas;
        [SerializeField] private GameObject workOnMech;
        [SerializeField] private TMP_Text constructionText;

        private void Start()
        {
            _mechFrame = FindObjectOfType<MechFrameSeller>();
            constructionCanvas.SetActive(false);
        }

        private void Update()
        {
            if (workOnMech.transform.childCount != 0 )
            {
                constructionCanvas.SetActive(true);
                constructionText.text = "Current Attributes: " +
                                        string.Join(", ", _mechFrame.GetMechAttributes());
            }
            else
            {
                constructionCanvas.SetActive(false);
            }
            
        }
    }
}
