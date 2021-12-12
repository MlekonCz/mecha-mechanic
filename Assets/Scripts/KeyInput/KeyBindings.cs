using UnityEngine;
using UnityEngine.Serialization;

namespace KeyInput
{
    [CreateAssetMenu(fileName = "KeyBindings", menuName = "KeyBindings", order = 0)]
    public class KeyBindings : ScriptableObject
    {
        
        [System.Serializable]
        public class KeyBindingCheck
        { 
            public KeyBindingActions _keyBindingActions;
            public KeyCode keyCode;
        }

        public KeyBindingCheck[] keybindingChecks;

    }
}