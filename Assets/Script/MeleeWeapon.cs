using System;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : Weapon
{

    private new Collider collider;


    private List<Actor> attackedActors = new();
    
    private void Start()
    {
        collider = GetComponent<Collider>();
        EndAttack();
    }

    public override void BeginAttack(Transform instigator)
    {
        attackedActors.Clear();
        collider.enabled = true;
    }

    public override void EndAttack()
    {
        collider.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Actor actor))
        {
            if (attackedActors.Contains(actor))
                return;
            actor.HpChange(-Owner.damage);
            attackedActors.Add(actor);
        }
    }
}