
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TS.Commons
{
    public class GameManager : Singleton<GameManager>
    {

        public event Action OnSceneLoadingStart;
        public event Action<float> OnSceneLoadingProgress;
        
        public void LoadScene(string name)
        {
            OnSceneLoadingStart?.Invoke();
            StartCoroutine(AssetManager.LoadSceneAsync(name, progress =>
            {
                OnSceneLoadingProgress?.Invoke(progress);
            }));
        }
        
        public void Quit()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}