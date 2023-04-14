using System;
using TS.Commons;
using UnityEngine;
using UnityEngine.UI;

namespace TS.UI
{
    public class LoadingUI : MonoBehaviour
    {
        public GameObject panel;
        public Slider slider;
        
        private void Start()
        {
            //订阅加载界面开始事件
            GameManager.Instance.OnSceneLoadingStart += OnSceneLoadingStart;
            //订阅场景加载进度事件
            GameManager.Instance.OnSceneLoadingProgress += OnSceneLoadingProgress;
            //此时将LoadingPanel设置为true，使其展示在界面上
            panel.gameObject.SetActive(false);
        }

        //实现加载界面开始事件
        private void OnSceneLoadingStart()
        {
            //并设置slider值为0
            panel.gameObject.SetActive(true);
            slider.value = 0f;
        }
        
        //实现场景加载进度事件
        private void OnSceneLoadingProgress(float progress)
        {
            //改变slider的值为当前加载数据的进度
            slider.value = progress;
        }

        private void OnDestroy()
        {
            //将事件订阅全部销毁
            GameManager.Instance.OnSceneLoadingStart -= OnSceneLoadingStart;
            GameManager.Instance.OnSceneLoadingProgress -= OnSceneLoadingProgress;
        }
        
    }
}