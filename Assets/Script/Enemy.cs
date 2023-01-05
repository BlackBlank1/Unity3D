using NodeCanvas.BehaviourTrees;
using NodeCanvas.Framework;
using UnityEngine;

public class Enemy : Actor
{
    
    private BehaviourTreeOwner tree;
    private Blackboard bb;
    private PlayerController player;
    
    public Weapon Weapon { get; protected set; }
    
    private void Start()
    {
        tree = GetComponent<BehaviourTreeOwner>();
        bb = GetComponent<Blackboard>();
        player = FindObjectOfType<PlayerController>();
        Weapon = GetComponentInChildren<Weapon>();
        Weapon.Owner = this;

        StartCoroutine(Util.Delay(1f, () =>
        {
            bb.SetVariableValue("Target", player);
        }));
        
    }

    public override void HpChange(float delta)
    {
        if (IsDead)
        {
            return;
        }
        hp += delta;

        if (delta < 0)
        {
            // tree.SendEvent("GetHit");
            bb.SetVariableValue("IsGettingHit", true);
        }
        
        if (hp <= 0)
        {
            IsDead = true;
            Die();
        }
    }

    protected override void Die()
    {
        bb.SetVariableValue("IsDead", true);
        gameObject.layer = LayerMask.NameToLayer("Void");
    }
}
