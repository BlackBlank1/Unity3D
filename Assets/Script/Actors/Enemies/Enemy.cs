using NodeCanvas.BehaviourTrees;
using NodeCanvas.Framework;
using TS.Actors.Player;
using TS.Battle;
using TS.Commons;
using UnityEngine;

namespace TS.Actors.Enemies
{

    public class Enemy : Actor
    {

        private BehaviourTreeOwner tree;
        private Blackboard bb;

        public Weapon Weapon { get; protected set; }

        protected override void Start()
        {
            base.Start();
            tree = GetComponent<BehaviourTreeOwner>();
            bb = GetComponent<Blackboard>();
            Weapon = GetComponentInChildren<Weapon>();
            Weapon.Owner = this;

            OnHpChanged += OnSelfHpChanged; //订阅事件
        }

        private void OnSelfHpChanged(float f, float maxHp, float delta)
        {
            bb.SetVariableValue("IsGettingHit", true);
            DamageNumberManager.Instance.ShowNumber(delta, transform.position);
        }


        protected override void Die()
        {
            base.Die();
            bb.SetVariableValue("IsDead", true);
            gameObject.layer = LayerMask.NameToLayer("Void");
        }

    }
}