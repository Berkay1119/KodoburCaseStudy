using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class ShootingState: EnemyState
{
    private Coroutine _shootRoutine;
    public ShootingState(global::Enemy enemy, NavMeshAgent navMeshAgent,Player player) : base(enemy, navMeshAgent, player)
    {
    }

    protected override void OnEnter()
    {
        _shootRoutine = Enemy.StartCoroutine(Enemy.ShootRoutine());
        NavMeshAgent.isStopped = true;
    }

    public override void OnUpdate()
    {
    }

    public override void OnExit()
    {
        if (Enemy.GetCurrentState() is ShootingState)
        {
            Enemy.StopCoroutine(_shootRoutine);
            NavMeshAgent.isStopped = false;
        }
        
    }
    
}
