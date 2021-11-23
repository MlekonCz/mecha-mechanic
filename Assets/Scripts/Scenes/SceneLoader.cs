using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scenes
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

        public void LoadScene(ScenesIndexes newScene, int lastScene)
        {
            SceneManager.UnloadSceneAsync(lastScene);
            SceneManager.LoadSceneAsync((int) newScene, LoadSceneMode.Additive);
            
        }

    }
}