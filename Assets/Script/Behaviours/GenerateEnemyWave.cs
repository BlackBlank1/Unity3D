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
            //拿到配置文件中的敌人总波次
            var wave = config.waves[currentWave.value];
            //循环波次进行敌人生成
            for (int i = 0; i < wave.num; i++)
            {
                //在一个圆域内随机生成敌人
                var circle = Random.insideUnitCircle;
                circle *= radius.value;
                var position = new Vector3(circle.x, 0, circle.y) + agent.transform.position;
                var raycast = Physics.Raycast(position, Vector3.down, out var hitInfo, float.PositiveInfinity, groundLayer.value);
                if (raycast)
                {
                    //获取敌人的prefab
                    GameObject[] enemyPrefabs = wave.enemyPrefabs; 
                    //生成敌人，GetRandomItem()获取到随机的敌人的prefab，生成随机敌人
                    GameObject go = Object.Instantiate(enemyPrefabs.GetRandomItem()); 
                    //设置敌人的位置
                    go.transform.position = hitInfo.point;
                    //设置每个生成敌人的伤害数值、生命值和最大生命值
                    Actor actor = go.GetComponent<Actor>();
                    actor.damage *= wave.damageMultiplier;
                    actor.hp *= wave.hpMultiplier;
                    actor.maxHp *= wave.hpMultiplier;
                    //将每个生成的敌人都添加进
                    agent.AddNewEnemy(actor);
                    //等待一个生成的间隔时间
                    yield return new WaitForSeconds(wave.spawnInterval);
                }
            }
            //等待一个波次生成时间
            yield return new WaitForSeconds(wave.waveInterval);
            //当前波次的值加1
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