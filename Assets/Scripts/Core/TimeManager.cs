using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

[ExecuteAlways]
public class TimeManager : MonoBehaviour
{
 private Light _directionalLight;
 [SerializeField] private LightningPreset _preset;
 [SerializeField] [Range(0, 24)] private float _timeOfDay;
 [SerializeField] private float _timeSlower = 2f;
 
 [Header("TimeUI")] [SerializeField] private TMP_Text _timeUI;
[SerializeField] private TMP_Text _daysUI;
 
 [SerializeField] public float currentDay = 1;
 private bool _newDay = false;

 public event Action onDayEnd;

 private void OnEnable()
 {
  if (GameObject.FindGameObjectWithTag("Light") != null)
  {
   _directionalLight = GameObject.FindGameObjectWithTag("Light").GetComponent<Light>();
  }
  
 }

 private void Update()
 { 
   if (_preset == null){return;}
   
    if (Application.isPlaying) 
    { 
     _timeOfDay += Time.deltaTime / _timeSlower; 
     _timeOfDay %= 24; 
     UpdateLightning(_timeOfDay/24f); 
    }
    else 
    { 
     UpdateLightning(_timeOfDay / 24f); 
    } 
    TimeTest(_timeOfDay / 24f); 
 }

 void UpdateLightning(float timePercent)
 {
  RenderSettings.ambientLight = _preset.ambientColor.Evaluate(timePercent);
  
  SkyboxChange(timePercent);

  if (_directionalLight != null)
  {
   _directionalLight.color = _preset.directionalColor.Evaluate(timePercent);
   _directionalLight.transform.localRotation = Quaternion.Euler(new Vector3((timePercent * 360)-90f,170f,0f));
   
  }
 }

 void TimeTest(float timeNormalized)
 {
  int dayTime = (int) (timeNormalized * 24);


  string hoursString = Mathf.Floor(dayTime).ToString("00");
  string minutesString = Mathf.Floor(((timeNormalized * 24 )%1f) * 60).ToString("00");
  _timeUI.text = hoursString + ":" + minutesString;
  _daysUI.text = currentDay.ToString();
  
  if (Math.Abs(timeNormalized - 1) < 0.01)
  {
   if (_newDay){return;}
   
   Invoke(nameof(NewDay),2f);
   _newDay = true;
   onDayEnd?.Invoke();
   currentDay += 1;
  }
 }

 void NewDay()
 { 
     _newDay = false;
 }
 void SkyboxChange(float time) 
 { 
      int dayTime = (int) (time * 24); 
      int skybox;
  
  
      if (dayTime < 5 || dayTime >= 20) 
      { 
       skybox = 3; 
      }
      else if (dayTime < 9) 
      { 
       skybox = 0; 
      }
      else if (dayTime <= 15)
 
      { 
       skybox = 1; 
      }
      else 
      { 
       skybox = 2; 
      }
      
      RenderSettings.skybox = _preset.skyboxes[skybox];
 }
 private void OnValidate()
 { 
   if (_directionalLight != null){return;}
   
   if (RenderSettings.sun != null) 
   { 
    _directionalLight = RenderSettings.sun; 
   }
   else 
   { 
     Light[] lights = GameObject.FindObjectsOfType<Light>();
    
     foreach (Light light in lights) 
     { 
      if (light.type == LightType.Directional) 
      { 
       _directionalLight = light; 
       return; 
      } 
     } 
   }
 }
}
