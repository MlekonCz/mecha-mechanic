using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core
{
    public enum ScenesIndexes
    {
        UIScene, 
        MainMenu,
        GameScene 
    }
    
    
    public class SceneLoader : MonoBehaviour
    {
        private void Awake()
        {
            SceneManager.LoadSceneAsync((int) ScenesIndexes.UIScene, LoadSceneMode.Additive);
        }

    }
}