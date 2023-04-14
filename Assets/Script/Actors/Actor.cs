using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace TS.Actors
{
    //因为在 Scene 视图中很难选中我们需要的节点，所以添加SelectionBase特性，这样的话，该对象的所有节点都会定位到绑定这个标记的对象身上
    [SelectionBase]
    public class Actor : MonoBehaviour
    {
        //hp值
        public float hp = 100f;

        //maxHp值
        public float maxHp = 100f;

        //防止重命名变量后丢失引用
        [FormerlySerializedAs("Damage")]
        public float damage = 10f;

        //判断是否死亡
        public bool IsDead { get; protected set; }
        
        //死亡事件
        public event Action<Actor> OnDead;

        public delegate void HpChangeDelegate(float hp, float maxHp, float delta); //委托

        public event HpChangeDelegate OnHpChanged; //生命值改变事件

        protected virtual void Start()
        {
            OnHpChanged?.Invoke(hp, maxHp, 0); //广播事件
        }

        public virtual void HpChange(float delta)
        {
            if (hp + delta > maxHp)
            {
                hp = maxHp;
            }
            if (IsDead)
            {
                return;
            }

            hp = Mathf.Clamp(hp + delta, 0, maxHp); //Clamp返回的是介于最大值和最小值之间的数
            OnHpChanged?.Invoke(hp, maxHp, delta);
            if (hp <= 0)
            {
                IsDead = true;
                Die();
            }
        }

        protected virtual void Die()
        {
            //死亡后，广播事件
            OnDead?.Invoke(this);
        }
    }
}