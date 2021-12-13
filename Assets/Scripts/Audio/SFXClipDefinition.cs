using Sirenix.OdinInspector;
using UnityEngine;

namespace Audio
{
    [CreateAssetMenu(fileName = "New SFX Clip", menuName = "Definition/NewSFXClip", order = 0)]
    public class SFXClipDefinition : ScriptableObject
    { 
        [InlineEditor(InlineEditorModes.FullEditor)]
        [Space]
        [Title("Audio Clip")]
        [Required]
        public AudioClip clip;

        [Title("Clip Setting")]
        [Range(0f,1f)]
        public float volume = 1f;
        
        [Range(0f,0.2f)]
        public float volumeVariation = 0.05f;
        
        [Range(0f,2f)]
        public float pitch = 1f;
        
        [Range(0f,0.2f)]
        public float pitchVariation = 1f;
    }
}