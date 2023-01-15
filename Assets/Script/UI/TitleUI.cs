using System;
using TS.Commons;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TS.UI
{
    public class TitleUI : MonoBehaviour
    {
        public void OnScreenClick()
        {
            GameManager.Instance.LoadScene("HomeScene");
        }
    }
}