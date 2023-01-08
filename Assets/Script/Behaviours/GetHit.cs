using System.Collections;
using NodeCanvas.Framework;
using TS.Actors;
using UnityEngine;
using UnityEngine.AI;

namespace TS.Behaviours
{

    public class GetHit : ActionTask<NavMeshAgent>
    {
        public BBParameter<float> animationDuration = 0.6f;

        private EnemyAnimator animator;
        private Coroutine getHitCoroutine;

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
            getHitCoroutine = StartCoroutine(DoGetHit());
        }

        // ReSharper disable Unity.PerformanceAnalysis
        private IEnumerator DoGetHit()
        {
            animator.PlayGitHit();
            var originIsStopped = agent.isStopped;
            agent.isStopped = true;
            yield return new WaitForSeconds(animationDuration.value);
            agent.isStopped = originIsStopped;
            EndAction(true);
        }

        // ReSharper disable Unity.PerformanceAnalysis
        protected override void OnStop()
        {
            base.OnStop();
            if (getHitCoroutine != null)
                StopCoroutine(getHitCoroutine);
        }
    }

}