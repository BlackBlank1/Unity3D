using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace TS.UI
{
    public class HomeUI : MonoBehaviour
    {
        [SerializeField]
        private Button battleButton;

        private void Start()
        {
            battleButton.onClick.AddListener(() =>
            {
                SceneManager.LoadScene("SampleScene");
            });
        }
    }
}