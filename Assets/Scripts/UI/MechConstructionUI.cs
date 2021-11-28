using System;
using MechParts;
using TMPro;
using UnityEngine;

namespace UI
{
    public class MechConstructionUI : MonoBehaviour
    {
        // private MechFrameSeller _mechFrame;
        // [SerializeField] private GameObject constructionCanvas;
        // [SerializeField] private TMP_Text constructionText;
        //
        // private MechFrameBuilder _mechFrameBuilder;
        // private bool _constructionIsInProgress = false;
        //
        //
        // private void Start()
        // {
        //     _mechFrame = FindObjectOfType<MechFrameSeller>();
        //     constructionCanvas.SetActive(false);
        // }
        //
        // private void OnEnable()
        // {
        //     if (_mechFrameBuilder == null)
        //     {
        //         _mechFrameBuilder = FindObjectOfType<MechFrameBuilder>();
        //         _mechFrame = FindObjectOfType<MechFrameSeller>();
        //     }
        //     
        //     if (_mechFrameBuilder == null){return;}
        //     _mechFrameBuilder.onConstructionStarted += StartConstruction;
        //     _mechFrameBuilder.onConstructionFinished += StopConstruction;
        // }
        //
        // private void Update()
        // {
        //     if (_constructionIsInProgress)
        //     {
        //         constructionCanvas.SetActive(true);
        //         constructionText.text = "Current Attributes: " +
        //                                 string.Join(", ", _mechFrame.GetMechAttributes());
        //     }
        //     else
        //     {
        //         constructionCanvas.SetActive(false);
        //     }
        //     
        // }
        //
        // private void StartConstruction()
        // {
        //     _constructionIsInProgress = true;
        // }
        //
        // private void StopConstruction()
        // {
        //     _constructionIsInProgress = false;
        // }
        //
        // private void OnDisable()
        // {
        //     if (_mechFrameBuilder == null) {return;}
        //     _mechFrameBuilder.onConstructionStarted -= StartConstruction;
        //     _mechFrameBuilder.onConstructionFinished -= StopConstruction;
        // }
    }
}
