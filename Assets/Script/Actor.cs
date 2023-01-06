using System;
using UnityEngine;

public class Actor : MonoBehaviour
{
    public float hp = 100f;
    
    public float Damage = 10f;

    public bool IsDead { get; protected set; }

    public event Action<Actor> OnDead; 

    public virtual void HpChange(float delta)
    {
        if (IsDead)
        {
            return;
        }
        hp += delta;
        if (hp <= 0)
        {
            IsDead = true;
            Die();
        }
    }

    protected virtual void Die()
    {
        OnDead?.Invoke(this);
    }
}