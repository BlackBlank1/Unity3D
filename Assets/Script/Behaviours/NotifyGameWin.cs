using NodeCanvas.Framework;
using TS.Actors.Enemies;

namespace TS.Behaviours
{
    public class NotifyGameWin : ActionTask<EnemyGenerator>
    {
        protected override void OnExecute()
        {
            base.OnExecute();
            agent.NotifyGameWin();
            EndAction(true);
        }
    }
}