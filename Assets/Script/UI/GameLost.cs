using System;
using TS.Actors;
using TS.Actors.Player;
using TS.Commons;
using UnityEngine;

namespace TS.UI
{
    public class GameLost : EndGamePanel
    {
        private void Awake()
        {
            var playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
            playerController.OnDead += OnDead;
        }

        private void OnDead(Actor obj)
        {
            StartCoroutine(Util.Delay(delay, () =>
            {
                Debug.Log("11");
                canvasGroup.alpha = 1;
                canvasGroup.blocksRaycasts = true;
            }));
        }
    }
}