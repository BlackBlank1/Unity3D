using TS.Actors.Enemies;
using TS.Actors.Player;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace TS.UI
{
    public class EndGamePanel : MonoBehaviour
    {
        public Button button;
        
        public float delay = 3.5f;
        
        public CanvasGroup canvasGroup;
        public void Start()
        {
            button.onClick.AddListener(() =>
            {
                SceneManager.LoadScene("HomeScene");
                Time.timeScale = 1;
            });
            canvasGroup = GetComponent<CanvasGroup>();
            canvasGroup.alpha = 0;
            canvasGroup.blocksRaycasts = false;
        }

    }
}