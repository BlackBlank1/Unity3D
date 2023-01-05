using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAnimator : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator animator;
    
    private static readonly int Speed = Animator.StringToHash("Speed");
    private static readonly int Attack = Animator.StringToHash("Attack");
    private static readonly int GetHit = Animator.StringToHash("GetHit");
    private static readonly int Die = Animator.StringToHash("Die");
    

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        animator.SetFloat(Speed, agent.velocity.magnitude);
    }

    public void PlayAttack()
    {
        animator.SetTrigger(Attack);
    }

    public void PlayGitHit()
    {
        animator.SetTrigger(GetHit);
    }

    public void IsDead()
    {
        animator.SetTrigger(Die);
    }
}
