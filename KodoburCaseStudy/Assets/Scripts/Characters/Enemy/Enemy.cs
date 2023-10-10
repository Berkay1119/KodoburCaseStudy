using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Character
{
    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] private Vector3[]patrolPoints;
    [SerializeField] private int attackDamage;
    [SerializeField] private EnemyAttackRange enemyAttackRange;
    private EnemyState _state;

    private void Awake()
    {
        enemyAttackRange.AdjustCollider(gameSettings.enemyAttackRadius);
    }

    private void Start()
    {
        _state = new PatrolState(this, navMeshAgent);
    }

    private void Update()
    {
        _state.OnUpdate();
    }

    protected override void Die()
    {
        EventManager.OnEnemyDied(this);
    }

    public int GetExperiencePoint()
    {
        return experiencePoint;
    }

    public IEnumerator ShootRoutine(Player player)
    {
        while (true)
        {
            player.TakeDamage(attackDamage);
            yield return new WaitForSeconds(gameSettings.enemyShootCooldown);
        }
    }

    public Vector3[] GetPatrolPoints()
    {
        return patrolPoints;
    }

    public void ChasePlayer(Player player)
    {
        ChangeState(new ChaseState(this,navMeshAgent,player));
    }
    
    public void ReturnToYourPatrol()
    {
        ChangeState(new PatrolState(this,navMeshAgent));
    }

    private void ChangeState(EnemyState newState)
    {
        _state.OnExit();
        _state = newState;
    }


    public EnemyState GetCurrentState()
    {
        return _state;
    }
}
