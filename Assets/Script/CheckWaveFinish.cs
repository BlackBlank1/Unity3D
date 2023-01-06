using NodeCanvas.Framework;

public class CheckWaveFinish : ConditionTask<EnemyGenerator>
{
    [BlackboardOnly]
    public BBParameter<int> currentWave = 0;

    protected override bool OnCheck()
    {
        return currentWave.value >= agent.levelConfig.waves.Length;
    }
}