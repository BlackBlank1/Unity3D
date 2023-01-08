using NodeCanvas.Framework;
using TS.Actors;

namespace TS.Behaviours
{

    public class CheckHasEnemy : ConditionTask<EnemyGenerator>
    {
        protected override bool OnCheck()
        {
            return agent.HasEnemy();
        }
    }

}