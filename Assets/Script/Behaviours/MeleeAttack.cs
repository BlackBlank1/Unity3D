using System.Collections;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace Script.Behaviours
{
    public class MeleeAttack : ActionTask<Enemy>
    {
        [RequiredField]
        public BBParameter<Actor> target;
        [Tooltip("攻击前摇")]
        public BBParameter<float> preDelay = 0.1f;
        [Tooltip("攻击时长")]
        public BBParameter<float> attackDuration = .1f;
        [Tooltip("攻击后摇")]
        public BBParameter<float> postDelay = .1f;

        private EnemyAnimator animator;
        private Coroutine attackCoroutine;
        
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
            if (target.value == null || target.value.IsDead)
            {
                target.value = null;
                EndAction(false);
                return;
            }
            attackCoroutine = StartCoroutine(DoAttack());
        }

        private IEnumerator DoAttack()
        {
            animator.PlayAttack();
            yield return new WaitForSeconds(preDelay.value);
            agent.Weapon.BeginAttack(agent.transform);
            yield return new WaitForSeconds(attackDuration.value);
            agent.Weapon.EndAttack();
            yield return new WaitForSeconds(postDelay.value);
            EndAction(true);
        }

        // ReSharper disable Unity.PerformanceAnalysis
        protected override void OnStop()
        {
            base.OnStop();
            if (attackCoroutine != null)
                StopCoroutine(attackCoroutine);
        }
    }
}