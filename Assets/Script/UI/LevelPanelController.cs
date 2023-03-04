using System;
using DG.Tweening;
using TS.Commons;
using UnityEngine;
using UnityEngine.UI;

namespace TS.UI
{
    public class LevelPanelController : MonoBehaviour
    {
        [SerializeField]
        private Button level1_button;
        [SerializeField]
        private Button level2_button;
        [SerializeField]
        private Button back_button;

        [SerializeField]
        private RectTransform levelDetail;

        [SerializeField]
        private RectTransform scrollView;

        [SerializeField]
        private GameObject levelDetailPanel;

        [SerializeField]
        private Button battle_button;

        [SerializeField]
        private Button cancel_button;
        
        [SerializeField]
        private Image[] images;

        [SerializeField]
        private Sprite[] sourceImages;

        private String sceneName = "HomeScene";

        private void Start()
        {
            //按钮设置与执行
            level1_button.onClick.AddListener(() =>
            {
                ShowLevelDetail();
                sceneName = "Level1";
                ImagesSetting();
            });
            level2_button.onClick.AddListener(() =>
            {
                ShowLevelDetail();
                sceneName = "Level2";
                ImagesSetting();
            });
            back_button.onClick.AddListener(() =>
            {
                gameObject.SetActive(false);
                HideLevelDetail();
            });

            battle_button.onClick.AddListener(() =>
            {
                GameManager.Instance.LoadScene(sceneName);
                AudioManager.Instance.PlayBattleMusic();
            });

            cancel_button.onClick.AddListener(() =>
            {
                HideLevelDetail();
            });
        }

        public void HideLevelDetail()
        {
            levelDetail.DOAnchorPos(new Vector2(0, 0), 0.5f);
            scrollView.DOAnchorPos(scrollView.pivot - levelDetail.pivot, 0.5f);
            levelDetailPanel.SetActive(false);
        }

        public void ShowLevelDetail()
        {
            levelDetail.DOAnchorPos(new Vector2(-levelDetail.rect.width, 0), 0.5f);
            scrollView.DOAnchorPos(scrollView.pivot - levelDetail.pivot + new Vector2(-levelDetail.rect.width/2, 0), 0.5f);
            levelDetailPanel.SetActive(true);
        }

        public void ImagesSetting()
        {
            if (sceneName == "Level1")
            {
                for (int i = 0; i < images.Length - 1; i++)
                {
                    images[i].sprite = sourceImages[i];
                }

                images[images.Length-1].sprite = sourceImages[^1];
            }
            else if (sceneName == "Level2")
            {
                for (int i = 0; i < images.Length; i++)
                {
                    images[i].sprite = sourceImages[i];
                }
            }
            else if (sceneName == "BossScene")
            {
                images[0].sprite = sourceImages[^1];
            }
        }
    }
}