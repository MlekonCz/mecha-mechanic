using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace Audio
{
    public class SFXManager : MonoBehaviour
    {
        private static SFXManager _instance;
      //  private SFXClipDefinition someList= new List<SFXClipDefinition>();

        public static SFXManager instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<SFXManager>();
                }
                return _instance;
            }
        }

        [HorizontalGroup("AudioSource")]
        public AudioSource defaultAudioSource;

        [TabGroup("Explosion")]
        public List<SFXClipDefinition> explosionSFX = new List<SFXClipDefinition>();

        [TabGroup("Shot")]
        public List<SFXClipDefinition> gunShotSFX;
        
        [TabGroup("Reload")]
        public List<SFXClipDefinition> reloadSFX;

        [Button]
        public  void UpdateList()
        {
          //  someList = (SFXClipDefinition) AssetDatabase.LoadAllAssetsAtPath("Assets/Audio/Explosions").ToList();
           
            explosionSFX = AssetDatabase.FindAssets("t:SFXClipDefinition", new string[] { "Assets/Audio/Explosions" })
                .Select(AssetDatabase.GUIDToAssetPath)
                .Select(AssetDatabase.LoadAssetAtPath<SFXClipDefinition>)
                .ToList();
            gunShotSFX = AssetDatabase.FindAssets("t:SFXClipDefinition", new string[] { "Assets/Audio/GunShots" })
                .Select(AssetDatabase.GUIDToAssetPath)
                .Select(AssetDatabase.LoadAssetAtPath<SFXClipDefinition>)
                .ToList();
            reloadSFX = AssetDatabase.FindAssets("t:SFXClipDefinition", new string[] { "Assets/Audio/Reloads" })
                .Select(AssetDatabase.GUIDToAssetPath)
                .Select(AssetDatabase.LoadAssetAtPath<SFXClipDefinition>)
                .ToList();
            //sfxArray = (SFXClipDefinition[]) AssetDatabase.LoadAllAssetsAtPath("Assets/Audio/Explosions");
            // explosionSFX = new List<SFXClipDefinition>(sfxArray);
        }
        public static void PlaySFX(SFXClipDefinition sfx, bool waitToFinish = true, AudioSource audioSource = null)
        {
            
            if (audioSource == null)
            {
                audioSource = SFXManager.instance.defaultAudioSource;
            }

            if (audioSource == null)
            {
                Debug.LogError("You forgot to add default audio source!");
                return;
            }

            if (!audioSource.isPlaying || !waitToFinish)
            {
                audioSource.clip = sfx.clip;
                audioSource.volume = sfx.volume + Random.Range(-sfx.volumeVariation, sfx.volumeVariation);
                audioSource.pitch = sfx.pitch + Random.Range(-sfx.pitchVariation, sfx.pitchVariation);
                audioSource.Play();
            }
        }
        [HorizontalGroup("AudioSource")]
        [ShowIf("@defaultAudioSource == null")]
        [GUIColor(0.4f,1f,0.5f,1)]
        [Button]
        void AddAudioSource()
        {
            defaultAudioSource = this.gameObject.GetComponent<AudioSource>();
            if (defaultAudioSource == null)
            {
                defaultAudioSource = this.gameObject.AddComponent<AudioSource>();
            }
        }
    }

 
    
}