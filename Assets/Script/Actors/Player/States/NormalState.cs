using UnityEngine;

namespace TS.Actors.Player.States
{
    public class NormalState : BaseState
    {
        public override void Execute()
        {
            base.Execute();
            p.HandleMovement();
            p.RotateTowardsAim(); // TODO 转向移动方向

            if (p.input.aim || p.input.fireDown)
            {
                m.ChangeState<PreAimState>();
            }
        }
        
    }
}