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

        private String sceneName = "HomeScene";

        private void Start()
        {
            level1_button.onClick.AddListener(() =>
            {
                ShowLevelDetail();
                sceneName = "Level1";
            });
            level2_button.onClick.AddListener(() =>
            {
                ShowLevelDetail();
                sceneName = "Level2";
            });
            back_button.onClick.AddListener(() =>
            {
                gameObject.SetActive(false);
            });

            battle_button.onClick.AddListener(() =>
            {
                GameManager.Instance.LoadScene(sceneName);
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
    }
}