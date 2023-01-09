﻿using TS.Actors.Enemies;
using TS.Commons;
using UnityEngine;

namespace TS.UI
{
    public class GameWin : EndGamePanel
    {
        private void Awake()
        {
            var enemyGenerator = FindObjectOfType<EnemyGenerator>();
            enemyGenerator.OnGameWin += OnGameWin;
        }

        private void OnGameWin()
        {
            StartCoroutine(Util.Delay(delay, () =>
            {
                canvasGroup.alpha = 1;
                canvasGroup.blocksRaycasts = true;
                Time.timeScale = 0;
            }));
        }
    }
}