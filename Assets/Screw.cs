using System;
using MechPartComponents;
using UnityEngine;
using UnityEngine.EventSystems;

public class Screw : MonoBehaviour
{
    [SerializeField]private GameObject _screwModel;
    
    [SerializeField] private float _screwingSpeed = 0.7f;
    [SerializeField] private float _screwingRotation = 2f;
    [SerializeField] private float _screwRange = 0.35f;

    [SerializeField] private bool _isScrewingBack = false;

    private bool _canBeManipulated = false;
    private PartComponent _partComponent;
    private float _startingHeight;
    private float currentHeight;

    private void OnEnable()
    {
        _partComponent._screwsCanBeManipulated += EnableManipulation;
    }

    private void Awake()
    {
        _partComponent = gameObject.transform.parent.GetComponentInParent<PartComponent>();
    }

    private void Start()
    {
        _startingHeight = gameObject.transform.localPosition.y;
    }

    private void EnableManipulation(bool canBeManipulated)
    {
        _canBeManipulated = canBeManipulated;
    }
    public void ScrewBolt()
    {
        if (!_canBeManipulated){return;}
        
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
            if (_partComponent != null)
            {
                _partComponent.ChangeStatusOfScrew(true);
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
            if (_partComponent != null)
            {
                _partComponent.ChangeStatusOfScrew(false);
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

    private void OnDisable()
    {
        
    }
}
