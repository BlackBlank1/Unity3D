using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    private List<Actor> enemyList = new ();

    public LevelConfig levelConfig;

    public void AddNewEnemy(Actor actor)
    {
        enemyList.Add(actor);
        actor.OnDead += OnDead;
    }

    private void OnDead(Actor actor)
    {
        enemyList.Remove(actor);
    }

    public bool HasEnemy()
    {
        return enemyList.Count != 0;
    }
}