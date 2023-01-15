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
            GameManager.Instance.OnSceneLoadingStart += OnSceneLoadingStart;
            GameManager.Instance.OnSceneLoadingProgress += OnSceneLoadingProgress;
            panel.gameObject.SetActive(false);
        }

        private void OnSceneLoadingStart()
        {
            panel.gameObject.SetActive(true);
            slider.value = 0f;
        }
        
        private void OnSceneLoadingProgress(float progress)
        {
            slider.value = progress;
        }

        private void OnDestroy()
        {
            GameManager.Instance.OnSceneLoadingStart -= OnSceneLoadingStart;
            GameManager.Instance.OnSceneLoadingProgress -= OnSceneLoadingProgress;
        }
        
    }
}