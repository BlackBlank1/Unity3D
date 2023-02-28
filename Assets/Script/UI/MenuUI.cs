using System;
using TMPro;
using TS.Commons;
using UnityEngine;
using UnityEngine.Audio;
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

        [SerializeField]
        private Slider soundVolume;
        
        [SerializeField]
        private Slider musicVolume;

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
            QualityDropdown.SetValueWithoutNotify(qualityLevel);
            QualityDropdown.onValueChanged.AddListener(OnQualityDropdownValueChanged);
            
            fullScreenToggle = GameObject.Find("FullScreenToggle").GetComponent<Toggle>();
            fullScreenToggle.onValueChanged.AddListener(FullScreen);
            
            //音乐
            musicVolume.value = AudioManager.Instance.GetMusicVolume();
            musicVolume.onValueChanged.AddListener(value => AudioManager.Instance.SetMusicVolume(value));
            
            //音效
            soundVolume.value = AudioManager.Instance.GetSoundVolume();
            soundVolume.onValueChanged.AddListener(value => AudioManager.Instance.SetSoundVolume(value));
        }

        public void FullScreen(bool isOn)
        {
            if (isOn)
            {
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