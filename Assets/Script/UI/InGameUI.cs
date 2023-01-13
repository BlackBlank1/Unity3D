using System;
using TS.Actors;
using TS.Actors.Player;
using TS.Commons;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace TS.UI
{
    public class InGameUI : MonoBehaviour
    {
        [SerializeField]
        private Button settingButton;

        [SerializeField]
        private Button backButton;

        [SerializeField]
        private Button resumeButton;

        [SerializeField]
        private Button quitButton;

        [SerializeField]
        private GameObject menuUI;
        
        public Slider playerHpSlider;

        private void Awake()
        {
            var playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
            playerController.OnHpChanged += OnPlayerHpChanged;
        }

        private void Start()
        {
            Scene scene = SceneManager.GetActiveScene();
            menuUI.SetActive(false);
            settingButton.onClick.AddListener(() =>
            {
                Time.timeScale = 0;
                menuUI.SetActive(true);
                PlayerInput.Instance.Invalidate();
                PlayerInput.Instance.enabled = false;
            });
            
            backButton.onClick.AddListener(() =>
            {
                menuUI.SetActive(false);
                StartCoroutine(Util.DelayFrames(4, () =>
                {
                    Time.timeScale = 1;
                    PlayerInput.Instance.enabled = true;
                }));
            });

            resumeButton.onClick.AddListener(() =>
            {
                SceneManager.LoadScene(scene.name);
                Time.timeScale = 1;
            });

            quitButton.onClick.AddListener(() =>
            {
                Quit();
            });
        }

        private void OnPlayerHpChanged(float hp, float maxHp, float delta)
        {
            playerHpSlider.value = hp / maxHp;
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