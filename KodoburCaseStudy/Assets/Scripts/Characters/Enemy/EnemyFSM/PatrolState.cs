
using UnityEngine;
using UnityEngine.AI;

public class PatrolState : EnemyState
{
    private Vector3[] _patrolPoints;
    private int _currentDestinationIndex;
    

    public PatrolState(Enemy enemy, NavMeshAgent navMeshAgent) : base(enemy, navMeshAgent)
    {
        
    }
    
    protected override void OnEnter()
    {
        _patrolPoints = _enemy.GetPatrolPoints();
        ReturnToTheFirstPatrolPoint();
    }

    private void ReturnToTheFirstPatrolPoint()
    {
        NavMeshAgent.destination = _patrolPoints[0];
        _currentDestinationIndex = 0;
    }

    public override void OnUpdate()
    {
        if (NavMeshAgent.remainingDistance<1)
        {
            _currentDestinationIndex++;
            NavMeshAgent.destination = _patrolPoints[_currentDestinationIndex % _patrolPoints.Length];
        }
    }

    public override void OnExit()
    {
        
    }


}
