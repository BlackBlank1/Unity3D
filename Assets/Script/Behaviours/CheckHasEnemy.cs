using NodeCanvas.Framework;
using TS.Actors;
using TS.Actors.Enemies;

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