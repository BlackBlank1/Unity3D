using TMPro;
using TS.Commons;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Button = UnityEngine.UI.Button;

namespace TS.UI
{
    public class HomeUI : MonoBehaviour
    {
        [SerializeField]
        private Button battleButton;

        [SerializeField]
        private Button settingButton;

        [SerializeField]
        private GameObject settingUI;

        [SerializeField]
        private TMP_Text rankText;

        [SerializeField]
        private TMP_Text expText;

        public Slider expSlider;

        public int currentExp;
        public int maxExp;

        private async void Start()
        {
            var playerData = await DataManager.Instance.ReadPlayerData();
            expSlider.interactable = false;
            rankText.text = playerData.level.ToString();
            expText.text = $"{currentExp.ToString()}/{maxExp.ToString()}";
            expSlider.value = (float)currentExp / maxExp;

            settingUI.SetActive(false);
            battleButton.onClick.AddListener(() => { GameManager.Instance.LoadScene("Level2"); });

            settingButton.onClick.AddListener((() => { settingUI.SetActive(true); }));
            UpdateExp(playerData.currentExp, playerData.maxExp, playerData.level);
        }
        
        private void UpdateExp(int currentExp, int maxExp, int level)
        {
            rankText.text = level.ToString();
            expText.text = $"{currentExp.ToString()}/{maxExp.ToString()}";
            expSlider.value = (float)currentExp / maxExp;
        }
    }
}
