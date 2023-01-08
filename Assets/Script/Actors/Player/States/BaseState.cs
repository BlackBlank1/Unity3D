using RobustFSM.Base;
using RobustFSM.Mono;

namespace TS.Actors.Player.States
{
    public abstract class BaseState : BState
    {

        protected PlayerController p;
        protected MonoFSM m;

        public override void Initialize()
        {
            base.Initialize();
            m = GetSuperMachine<MonoFSM>();
            p = m.GetComponent<PlayerController>();
        }
        
    }
}