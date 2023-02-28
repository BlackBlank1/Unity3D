using TS.Actors.Enemies;
using TS.Actors.Player;
using TS.Commons;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace TS.UI
{
    public class EndGamePanel : MonoBehaviour
    {
        //确定button
        public Button button;
        
        public float delay = 5f;
        
        public CanvasGroup canvasGroup;
        public void Start()
        {
            button.onClick.AddListener(() =>
            {
                GameManager.Instance.LoadScene("HomeScene");
                AudioManager.Instance.PlayTitleMusic();
                Time.timeScale = 1;
            });
            canvasGroup = GetComponent<CanvasGroup>();
            canvasGroup.alpha = 0;
            canvasGroup.blocksRaycasts = false;
        }

    }
}