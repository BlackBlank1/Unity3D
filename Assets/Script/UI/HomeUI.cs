using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
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
        private void Start()
        {
            settingUI.SetActive(false);
            battleButton.onClick.AddListener(() =>
            {
                SceneManager.LoadScene("GameScene");
            });
            
            settingButton.onClick.AddListener((() =>
            {
                settingUI.SetActive(true);
            }));
        }
    }
}