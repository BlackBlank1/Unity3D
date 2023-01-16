using System;
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

        private void Start()
        {
            level1_button.onClick.AddListener(() =>
            {
                GameManager.Instance.LoadScene("Level1");
            });
            level2_button.onClick.AddListener(() =>
            {
                GameManager.Instance.LoadScene("Level2");
            });
            back_button.onClick.AddListener(() =>
            {
                gameObject.SetActive(false);
            });
        }
    }
}