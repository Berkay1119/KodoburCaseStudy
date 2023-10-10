using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyState
{
    protected Enemy _enemy;
    protected readonly NavMeshAgent NavMeshAgent;
    protected EnemyState(Enemy enemy, NavMeshAgent navMeshAgent)
    {
        _enemy=enemy;
        NavMeshAgent = navMeshAgent;
        OnEnter();
    }

    protected abstract void OnEnter();

    public abstract void OnUpdate();

    public abstract void OnExit();
}
