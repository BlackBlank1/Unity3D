using System;
using UnityEngine;
using UnityEngine.Serialization;

public class Actor : MonoBehaviour
{
    public float hp = 100f;

    public float maxHp = 100f;
    
    [FormerlySerializedAs("Damage")]
    public float damage = 10f;

    public bool IsDead { get; protected set; }
    public event Action<Actor> OnDead;

    public delegate void HpChangeDelegate(float hp, float maxHp, float delta);   //委托

    public event HpChangeDelegate OnHpChanged;   //事件

    protected virtual void Start()
    {
        OnHpChanged?.Invoke(hp, maxHp, 0);    //广播事件
    }

    public virtual void HpChange(float delta)
    {
        if (IsDead)
        {
            return;
        }
        hp = Mathf.Clamp(hp + delta, 0, maxHp);
        OnHpChanged?.Invoke(hp, maxHp, delta);
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