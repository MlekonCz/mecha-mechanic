using UnityEngine;

namespace KeyInput
{
    public class InputManager : MonoBehaviour
    {
        // put this script on persistent object

        [SerializeField] private KeyBindings _keyBindings;

        public KeyCode GetKeyForAction(KeyBindingActions keyBindingActions)
        {
            foreach (KeyBindings.KeyBindingCheck keyBindingCheck in _keyBindings.keybindingChecks)
            {
                if (keyBindingCheck._keyBindingActions == keyBindingActions)
                {
                    return keyBindingCheck.keyCode;
                }
            }
            
            
            return KeyCode.None;
        }

        public bool GetKeyDown(KeyBindingActions key)
        { 
            foreach (KeyBindings.KeyBindingCheck keyBindingCheck in _keyBindings.keybindingChecks)
            {
                if (keyBindingCheck._keyBindingActions == key)
                {
                    return Input.GetKeyDown(keyBindingCheck.keyCode);
                }
            }
            return false;
        }
        public bool GetKey(KeyBindingActions key)
        { 
            foreach (KeyBindings.KeyBindingCheck keyBindingCheck in _keyBindings.keybindingChecks)
            {
                if (keyBindingCheck._keyBindingActions == key)
                {
                    return Input.GetKey(keyBindingCheck.keyCode);
                }
            }
            return false;
        }
        public bool GetKeyUp(KeyBindingActions key)
        { 
            foreach (KeyBindings.KeyBindingCheck keyBindingCheck in _keyBindings.keybindingChecks)
            {
                if (keyBindingCheck._keyBindingActions == key)
                {
                    return Input.GetKeyUp(keyBindingCheck.keyCode);
                }
            }
            return false;
        }
    }
}