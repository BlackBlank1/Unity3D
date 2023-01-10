using System.Collections;
using System.Collections.Generic;
using NodeCanvas.Framework;
using TS.Actors;
using TS.Actors.Enemies;
using TS.Battle;
using TS.Commons;
using UnityEngine;
using UnityEngine.AI;

namespace TS.Behaviours
{

    public class GenerateEnemyWave : ActionTask<EnemyGenerator>
    {
        [BlackboardOnly]
        public BBParameter<int> currentWave = 0;

        private LevelConfig config => agent.levelConfig;

        public BBParameter<float> radius = 5f;
        public BBParameter<LayerMask> groundLayer;

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
                var circle = Random.insideUnitCircle;
                circle *= radius.value;
                var position = new Vector3(circle.x, 0, circle.y) + agent.transform.position;
                var raycast = Physics.Raycast(position, Vector3.down, out var hitInfo, float.PositiveInfinity, groundLayer.value);
                if (raycast)
                {
                    GameObject[] enemyPrefabs = wave.enemyPrefabs; //获取敌人的prefab
                    GameObject go = Object.Instantiate(enemyPrefabs.GetRandomItem()); //生成敌人
                    go.transform.position = hitInfo.point;
                    Actor actor = go.GetComponent<Actor>();
                    actor.damage = wave.damageMultiplier;
                    actor.hp *= wave.hpMultiplier;
                    actor.maxHp *= wave.hpMultiplier;
                    agent.AddNewEnemy(actor);
                    yield return new WaitForSeconds(wave.spawnInterval);
                }
            }
            yield return new WaitForSeconds(wave.waveInterval);
            currentWave.value++;
            EndAction(true);
        }

        public override void OnDrawGizmosSelected()
        {
            base.OnDrawGizmosSelected();
            Gizmos.color = new Color(1f, 1f, 1f, 0.6f);
            Gizmos.DrawSphere(agent.transform.position, radius.value);
        }
    }

}