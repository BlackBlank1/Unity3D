using System;
using System.Collections.Generic;
using NodeCanvas.Framework;
using TS.Battle;
using UnityEngine;

namespace TS.Actors.Enemies
{

    public class EnemyGenerator : MonoBehaviour
    {
        private List<Actor> enemyList = new();

        public LevelConfig levelConfig;

        private Blackboard blackboard;

        public event Action OnFinished;

        private void Start()
        {
            blackboard = GetComponent<Blackboard>();
        }

        public void RandomGenerator()
        {
            blackboard.SetVariableValue("IsStarted", true);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                blackboard.SetVariableValue("IsStarted", true);
                GetComponent<Collider>().enabled = false;
            }
        }

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
            OnFinished?.Invoke();
        }
    }

}