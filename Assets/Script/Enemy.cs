using NodeCanvas.BehaviourTrees;
using NodeCanvas.Framework;
using UnityEngine;

public class Enemy : Actor
{
    
    private BehaviourTreeOwner tree;
    private Blackboard bb;
    private PlayerController player;
    
    public Weapon Weapon { get; protected set; }
    
    protected override void Start()
    {
        base.Start();
        tree = GetComponent<BehaviourTreeOwner>();
        bb = GetComponent<Blackboard>();
        player = FindObjectOfType<PlayerController>();
        Weapon = GetComponentInChildren<Weapon>();
        Weapon.Owner = this;

        StartCoroutine(Util.Delay(1f, () =>
        {
            bb.SetVariableValue("Target", player);
        }));
        OnHpChanged += OnSelfHpChanged;
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
