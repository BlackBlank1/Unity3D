using System;
using TMPro;
using TS.Commons;
using UnityEngine;
using UnityEngine.UI;

namespace TS.UI
{
    public class MenuUI : MonoBehaviour
    {
        [SerializeField]
        private Button backButton;

        [SerializeField]
        private Button quitButton;
        
        public TMP_Dropdown QualityDropdown;

        private Toggle fullScreenToggle;
        
        private FullScreenMode fullScreenMode;
        
        private int qualityLevel;

        private void Start()
        {
            backButton.onClick.AddListener((() =>
            {
                gameObject.SetActive(false);
            })); 
            quitButton.onClick.AddListener((() =>
            {
                GameManager.Instance.Quit();
            }));
            qualityLevel = QualitySettings.GetQualityLevel();
            fullScreenToggle = GameObject.Find("FullScreenToggle").GetComponent<Toggle>();
            QualityDropdown.onValueChanged.AddListener(OnQualityDropdownValueChanged);
            fullScreenToggle.onValueChanged.AddListener(FullScreen);
        }

        public void FullScreen(bool isOn)
        {
            if (isOn)
            {
                Debug.Log("111");
                //设置当前分辨率
                var fullResolution = Screen.resolutions[^1];
                fullScreenMode = Screen.fullScreenMode;
                Screen.SetResolution(fullResolution.width, fullResolution.height, true);
                //设置成满屏
                Screen.fullScreen = true;
            }
        }
        
        private void OnQualityDropdownValueChanged(int value)
        {
            QualitySettings.SetQualityLevel(value);
        }
    }
}