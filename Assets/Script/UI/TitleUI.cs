using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TS.UI
{
    public class TitleUI : MonoBehaviour
    {
        public void OnScreenClick()
        {
            SceneManager.LoadScene("HomeScene");
        }
    }
}