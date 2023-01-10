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
            Debug.Log("222");
            battleButton.onClick.AddListener(() =>
            {
                Debug.Log("11111");
                SceneManager.LoadScene("GameScene");
            });
        }
    }
}