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
        public int winExp;
        public int lostExp;
        public int rank;

        private async void Start()
        {
            expSlider.interactable = false;
            rankText.text = rank.ToString();
            expText.text = $"{currentExp.ToString()}/{maxExp.ToString()}";
            expSlider.value = (float)currentExp / maxExp;

            settingUI.SetActive(false);
            battleButton.onClick.AddListener(() => { GameManager.Instance.LoadScene("GameScene"); });

            settingButton.onClick.AddListener((() => { settingUI.SetActive(true); }));
            var playerData = await DataManager.Instance.ReadPlayerData();
            UpdateExp(playerData.currentExp, playerData.maxExp);
        }
        
        private void UpdateExp(int currentExp, int maxExp)
        {
            rankText.text = rank.ToString();
            expText.text = $"{currentExp.ToString()}/{maxExp.ToString()}";
            expSlider.value = (float)currentExp / maxExp;
        }
    }
}
