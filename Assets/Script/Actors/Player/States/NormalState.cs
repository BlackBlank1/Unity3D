using UnityEngine;

namespace TS.Actors.Player.States
{
    public class NormalState : BaseState
    {
        public override void Execute()
        {
            base.Execute();
            p.HandleMovement();
            p.RotateTowardsMovement();

            if (p.input.aim || p.input.fireDown)
            {
                m.ChangeState<PreAimState>();
            }
        }
        
    }
}