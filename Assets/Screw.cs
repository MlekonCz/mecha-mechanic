using System;
using MechPartComponents;
using UnityEngine;
using UnityEngine.EventSystems;

public class Screw : MonoBehaviour
{
    [SerializeField]private GameObject _screwModel = null;
    
    [SerializeField] private float _screwingSpeed = 0.7f;
    [SerializeField] private float _screwingRotation = 2f;
    [SerializeField] private float _screwRange = 0.35f;

    [SerializeField] private bool _isScrewingBack = false;

    private PartComponent partComponent;
    private float _startingHeight;
    private float currentHeight;

    private void Start()
    {
        _startingHeight = gameObject.transform.localPosition.y;
        partComponent = GetComponentInParent<PartComponent>();
    }
    public void ScrewBolt()
    { 
        currentHeight = gameObject.transform.localPosition.y;
       
        if (_isScrewingBack)
        {
            TightenBolt();
        }
        else
        {
          LoosenBolt();
        }
       
    }

    private void TightenBolt()
    {
        if (currentHeight <= _startingHeight)
        {
            if (partComponent != null)
            {
                partComponent.ChangeStatusOfScrew(true);
            }
            Debug.Log("Screwed in");
            return;
        }
        Screwing(true);
    }

    private void LoosenBolt()
    {
        if (currentHeight >= _startingHeight + _screwRange)
        {
            if (partComponent != null)
            {
                partComponent.ChangeStatusOfScrew(false);
            }
            
            Debug.Log("Screwed out");
            _screwModel.SetActive(false);
            return;
        }
        
        Screwing(false);
    }
    
    
    private void Screwing(bool screw)
    {
        if (screw)
        {
            gameObject.transform.Rotate(0, -_screwingRotation * Time.deltaTime, 0);
            gameObject.transform.Translate(0, -_screwingSpeed * Time.deltaTime, 0, Space.Self);
        }
        else
        {
            gameObject.transform.Rotate(0, _screwingRotation * Time.deltaTime, 0);
            gameObject.transform.Translate(0, _screwingSpeed * Time.deltaTime, 0, Space.Self);
           
        }
       
    }
}
