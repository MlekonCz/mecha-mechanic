using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace DefaultNamespace
{
    public enum SFXType
    {
        Explosion,
        GunShot,
        Reload
    }
    [System.Serializable]
    public class SFX
    {
        [PropertyOrder(-2)]
        [LabelText("SFX Type")]
        [LabelWidth(100)]
        [OnValueChanged("SFXChange")]
        [InlineButton("PlaySFX")]
        public SFXType sfxType = DefaultNamespace.SFXType.Explosion;
        
        [PropertyOrder(-2)]
        [LabelText("$sfxLabel")]
        [LabelWidth(100)]
        [ValueDropdown("SFXType")]
        [OnValueChanged("SFXChange")]
        [InlineButton("SelectSFX")]
        public SFXClipDefinition sfxToPlay;
        private string sfxLabel = "SFX";
        
       
      

       
        [SerializeField] private bool showSettings = false;
   
       
        [SerializeField] private bool editSettings = false;
        
        [InlineEditor(InlineEditorObjectFieldModes.Hidden)]
        [ShowIf("showSettings")]
        [EnableIf("editSettings")]
        [SerializeField] private SFXClipDefinition _sfxBase;

        [Title("Audio Source")]
        [ShowIf("showSettings")]
        [EnableIf("editSettings")]
        [SerializeField] private AudioSource audioSource;
        
        [ShowIf("showSettings")]
        [EnableIf("editSettings")]
        [SerializeField] private bool waitToPlay = true;
        
        [ShowIf("showSettings")]
        [EnableIf("editSettings")]
        [SerializeField] private bool useDefault = true;
        
     
        [PropertyOrder(-1)]
        [Button]
        void UpdateSFXLists()
        {
            SFXManager.instance.UpdateList();
        }
        
        private void SFXChange()
        {
            sfxLabel = sfxType.ToString() + " SFX";

            _sfxBase = sfxToPlay;
        }

        void SelectSFX()
        {
            UnityEditor.Selection.activeObject = sfxToPlay;
        }

        private List<SFXClipDefinition> SFXType()
        {
            List<SFXClipDefinition> sfxList = new List<SFXClipDefinition>();
        
            switch (sfxType)
            {
                case DefaultNamespace.SFXType.Explosion:
                    sfxList = SFXManager.instance.explosionSFX;
                    break;
                case DefaultNamespace.SFXType.GunShot:
                    sfxList = SFXManager.instance.gunShotSFX;
                    break;
                case DefaultNamespace.SFXType.Reload:
                    sfxList = SFXManager.instance.reloadSFX;
                    break;
                default:
                    break;
            }

            return sfxList;
        }
        public void PlaySFX()
        {
            if (useDefault || audioSource ==null)
            {
                SFXManager.PlaySFX(sfxToPlay, waitToPlay, null);
            }

            else
            {
                SFXManager.PlaySFX(sfxToPlay, waitToPlay, audioSource);  
            }
        }
    }
    
}