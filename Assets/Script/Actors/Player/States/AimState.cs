using TS.Commons;
using UnityEngine;

namespace TS.Actors.Player.States
{
    public class AimState : BaseState
    {

        public override void Execute()
        {
            base.Execute();
            p.HandleMovement();
            p.RotateTowardsAim();
            
            p.aimLine.Show(p.gun.firePoint.position, p.aimDirection);

            if (p.input.fire || p.input.fireDown)
            {
                p.animator.SetBool(AnimID.IsFiring, true);
                p.gun.Fire(p.transform);
                p.input.fireDown = false;
            }
            else
            {
                p.animator.SetBool(AnimID.IsFiring, false);
            }
            
            if (!p.input.aim && !p.input.fire)
            {
                m.ChangeState<NormalState>();
            }
        }

        public override void Exit()
        {
            base.Exit();
            p.animator.SetBool(AnimID.IsAiming, false);
            p.animator.SetBool(AnimID.IsFiring, false);
            p.aimLine.Hide();
        }
        
    }
}