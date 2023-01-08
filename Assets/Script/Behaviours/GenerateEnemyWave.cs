using System.Collections;
using System.Collections.Generic;
using NodeCanvas.Framework;
using TS.Actors;
using TS.Actors.Enemies;
using TS.Battle;
using UnityEngine;
using UnityEngine.AI;

namespace TS.Behaviours
{

    public class GenerateEnemyWave : ActionTask<EnemyGenerator>
    {
        public Transform[] spawnPoints;

        [BlackboardOnly]
        public BBParameter<int> currentWave = 0;

        private LevelConfig config => agent.levelConfig;

        // ReSharper disable Unity.PerformanceAnalysis
        protected override void OnExecute()
        {
            base.OnExecute();
            StartCoroutine(DoWave());
        }

        private IEnumerator DoWave()
        {
            var wave = config.waves[currentWave.value];
            for (int i = 0; i < wave.num; i++)
            {
                GameObject[] enemyPrefabs = wave.enemyPrefabs; //获取敌人的prefab
                int index1 = Random.Range(0, enemyPrefabs.Length); //随机敌人
                int index2 = Random.Range(0, spawnPoints.Length); //随机敌人生成点
                GameObject go = Object.Instantiate(enemyPrefabs[index1], spawnPoints[index2].transform); //生成敌人
                Actor actor = go.GetComponent<Actor>();
                actor.damage = wave.damageMultiplier;
                actor.hp *= wave.hpMultiplier;
                actor.maxHp *= wave.hpMultiplier;
                agent.AddNewEnemy(actor);
                yield return new WaitForSeconds(wave.spawnInterval);
            }

            yield return new WaitForSeconds(wave.waveInterval);
            currentWave.value++;
            EndAction(true);
        }
    }

}