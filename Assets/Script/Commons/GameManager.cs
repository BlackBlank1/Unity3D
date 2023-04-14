
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TS.Commons
{
    public class GameManager : Singleton<GameManager>
    {
        //场景跳转事件
        public event Action OnSceneLoadingStart;
        //场景加载进度事件
        public event Action<float> OnSceneLoadingProgress;
        
        public void LoadScene(string name)
        {
            //广播场景跳转开始事件
            OnSceneLoadingStart?.Invoke();
            //开启协程，广播场景加载进度事件
            StartCoroutine(AssetManager.LoadSceneAsync(name, progress =>
            {
                OnSceneLoadingProgress?.Invoke(progress);
            }));
        }
        
        public void Quit()
        {
            //如果是在unity引擎上，则退出运行状态
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            //如果在其他平台，则直接退出游戏
            Application.Quit();
#endif
        }
    }
}