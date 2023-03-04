using TS.Actors.Enemies;
using TS.Commons;
using UnityEngine;

namespace TS.UI
{
    public class GameWin : EndGamePanel
    {
        private void Awake()
        {
            var director = FindObjectOfType<Director>();
            director.OnGameWin += OnGameWin;
        }
        
        private void OnGameWin()
        {
            StartCoroutine(Util.Delay(delay, () =>
            {
                //将界面显示
                canvasGroup.alpha = 1;
                canvasGroup.blocksRaycasts = true;
                //游戏内动画等都暂停
                Time.timeScale = 0;
            }));
        }
    }
}