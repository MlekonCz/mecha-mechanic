using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[ExecuteAlways]
public class TimeManager : MonoBehaviour
{
 [SerializeField] private Light directionalLight;
 [SerializeField] private LightningPreset preset;
 [SerializeField] [Range(0, 24)] private float timeOfDay;
 [SerializeField] private float timeSlower = 2f;

 [Header("TimeUI")] 
 [SerializeField] private TMP_Text timeUI;
 [SerializeField] private TMP_Text daysUI;
 
 [SerializeField] public float currentDay = 1;
 private bool _newDay = false;
 
 private void Update()
 {
  
  
  if (preset == null){return;}

  if (Application.isPlaying)
  {
   timeOfDay += Time.deltaTime / timeSlower;
   timeOfDay %= 24;
   UpdateLightning(timeOfDay/24f);
  }
  else
  {
   UpdateLightning(timeOfDay / 24f);
  }
  TimeTest(timeOfDay / 24f);
 }

 void UpdateLightning(float timePercent)
 {
  RenderSettings.ambientLight = preset.ambientColor.Evaluate(timePercent);
  
  SkyboxChange(timePercent);

  if (directionalLight != null)
  {
   directionalLight.color = preset.directionalColor.Evaluate(timePercent);
   directionalLight.transform.localRotation = Quaternion.Euler(new Vector3((timePercent * 360)-90f,170f,0f));
   
  }
 }

 void TimeTest(float timeNormalized)
 {
  int dayTime = (int) (timeNormalized * 24);


  string hoursString = Mathf.Floor(dayTime).ToString("00");
  string minutesString = Mathf.Floor(((timeNormalized * 24 )%1f) * 60).ToString("00");
  timeUI.text = hoursString + ":" + minutesString;
  daysUI.text = currentDay.ToString();
  
  if (Math.Abs(timeNormalized - 1) < 0.01)
  {
   if (_newDay){return;}
   Invoke(nameof(NewDay),2f);
   _newDay = true;
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
  
  RenderSettings.skybox = preset.skyboxes[skybox];
 }
 private void OnValidate()
 {
  if (directionalLight != null){return;}

  if (RenderSettings.sun != null)
  {
   directionalLight = RenderSettings.sun;
  }
  else
  { 
   Light[] lights = GameObject.FindObjectsOfType<Light>();
   
   foreach (Light light in lights)
   { 
    if (light.type == LightType.Directional)
    {
     directionalLight = light;
     return; 
    }
   }
  }
  
  
 }
}
