using System;
using UnityEngine;
using UnityEngine.UI;

namespace TS.UI
{
    public class MenuUI : MonoBehaviour
    {
        [SerializeField]
        private Button backButton;

        private void Start()
        {
            backButton.onClick.AddListener((() =>
            {
                gameObject.SetActive(false);
            }));
        }
    }
}