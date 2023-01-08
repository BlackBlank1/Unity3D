using System.Collections;
using TS.Commons;
using UnityEngine;

namespace TS.Actors.Player.States
{
    
    /// <summary>
    /// 从正常状态过渡到瞄准状态
    /// </summary>
    public class PreAimState : BaseState
    {

        private Coroutine coroutine;
        
        public override void Enter()
        {
            base.Enter();
            p.animator.SetBool(AnimID.IsAiming, true);
            
            coroutine = p.StartCoroutine(DoPreAim());
        }

        public override void Execute()
        {
            base.Execute();
            p.HandleMovement();
            p.RotateTowardsAim(p.normalTurnSpeed);
        }

        private IEnumerator DoPreAim()
        {
            yield return new WaitForSeconds(p.fireDelay);
            coroutine = null;

            m.ChangeState<AimState>();
        }

        public override void Exit()
        {
            base.Exit();
            if (coroutine != null)
                p.StopCoroutine(coroutine);
        }
        
    }
}