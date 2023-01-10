using System;
using TS.Actors.Enemies;
using UnityEngine;

namespace TS.Commons
{
    public class Director : MonoBehaviour
    {
        private float count;
        
        public event Action OnGameWin;
        private void Awake()
        {
            var enemyGenerators = FindObjectsOfType<EnemyGenerator>();
            count = enemyGenerators.Length;
            foreach (var i in enemyGenerators)
            {
                i.OnFinished += OnFinished;
            }
        }

        private void OnFinished()
        {
            count -= 1;
            if (count == 0)
            {
                OnGameWin?.Invoke();
            }
        }
    }
}