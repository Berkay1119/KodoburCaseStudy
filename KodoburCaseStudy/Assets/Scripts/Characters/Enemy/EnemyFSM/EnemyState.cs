using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyState
{
    protected Enemy Enemy;
    protected readonly NavMeshAgent NavMeshAgent;
    protected Player Player;
    protected EnemyState(Enemy enemy, NavMeshAgent navMeshAgent, Player player)
    {
        Enemy=enemy;
        NavMeshAgent = navMeshAgent;
        Player = player;
        OnEnter();
    }

    protected abstract void OnEnter();

    public abstract void OnUpdate();

    public abstract void OnExit();
}
