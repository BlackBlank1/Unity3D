using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace TS.Actors.Enemies
{
    public class Generator : MonoBehaviour
    {
        [SerializeField]
        private EnemyGenerator[] enemyGenerators;

        [SerializeField]
        private float generatorSpan;

        private float cur = 0;

        private void Update()
        {
            if (cur >= generatorSpan)
            {
                RandomEnemyGenerator();
                cur = 0;
            }
            cur += Time.deltaTime;
        }

        private void RandomEnemyGenerator()
        {
            var range = Random.Range(0, enemyGenerators.Length);
            enemyGenerators[range].RandomGenerator();
        }
    }
}