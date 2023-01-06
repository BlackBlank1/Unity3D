using UnityEngine;


[CreateAssetMenu(fileName = "LevelConfig", menuName = "Unity3D/LevelConfig", order = 0)]
public class LevelConfig : ScriptableObject
{
    public Wave[] waves;
}

[System.Serializable]
public class Wave
{
    //数量
    public int num;
    //血量增加系数
    public float hpMultiplier;
    //伤害增加系数
    public float damageMultiplier;
    //敌人生成间隔
    public float spawnInterval;
    //波次生成间隔
    public float waveInterval;
    //敌人prefab
    public GameObject[] enemyPrefabs;
}