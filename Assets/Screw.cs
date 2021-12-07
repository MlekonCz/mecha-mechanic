using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Screw : MonoBehaviour
{
    [SerializeField] private float _screwingSpeed = 1f;
    
    private void OnMouseEnter()
    {
        Debug.Log("Iam gonna be Screwed");
    }

    private void OnMouseExit()
    {
        Debug.Log("I survived, for now");
    }

    public void ScrewOff()
    {
        Debug.Log("Screw you!");
        gameObject.transform.Translate(0,_screwingSpeed*Time.deltaTime,0,Space.Self);
    }

    
}
