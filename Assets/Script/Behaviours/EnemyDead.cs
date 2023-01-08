using System.Collections;
using System.Security.Cryptography;
using NodeCanvas.Framework;
using TS.Actors;
using TS.Actors.Enemies;
using UnityEngine;
using UnityEngine.AI;

namespace TS.Behaviours
{

    public class EnemyDead : ActionTask<NavMeshAgent>
    {
        public BBParameter<float> animationDuration = 0.6f;

        private EnemyAnimator animator;
        private Coroutine deadCoroutine;

        // ReSharper disable Unity.PerformanceAnalysis
        protected override string OnInit()
        {
            animator = agent.GetComponent<EnemyAnimator>();
            return base.OnInit();
        }

        // ReSharper disable Unity.PerformanceAnalysis
        protected override void OnExecute()
        {
            base.OnExecute();
            deadCoroutine = StartCoroutine(Dead());
        }

        private IEnumerator Dead()
        {
            animator.IsDead();
            var originIsStopped = agent.isStopped;
            agent.isStopped = true;
            yield return new WaitForSeconds(animationDuration.value);
            agent.isStopped = originIsStopped;
            EndAction(true);
            Object.Destroy(agent.gameObject);
        }
    }

}