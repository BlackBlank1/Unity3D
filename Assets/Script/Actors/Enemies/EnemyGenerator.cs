using System;
using System.Collections.Generic;
using TS.Battle;
using UnityEngine;

namespace TS.Actors.Enemies
{

    public class EnemyGenerator : MonoBehaviour
    {
        private List<Actor> enemyList = new();

        public LevelConfig levelConfig;

        public event Action OnGameWin; 

        public void AddNewEnemy(Actor actor)
        {
            enemyList.Add(actor);
            actor.OnDead += OnDead;
        }

        private void OnDead(Actor actor)
        {
            enemyList.Remove(actor);
        }

        public bool HasEnemy()
        {
            return enemyList.Count != 0;
        }

        public void NotifyGameWin()
        {
            OnGameWin?.Invoke();
        }
    }

}