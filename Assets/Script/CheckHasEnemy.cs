using NodeCanvas.Framework;

public class CheckHasEnemy : ConditionTask<EnemyGenerator>  
{
    protected override bool OnCheck()
    {
        return agent.HasEnemy();
    }
}