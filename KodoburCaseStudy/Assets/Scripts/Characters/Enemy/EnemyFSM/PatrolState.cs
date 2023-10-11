
using UnityEngine;
using UnityEngine.AI;

public class PatrolState : EnemyState
{
    private Transform[] _patrolPoints;
    private int _currentDestinationIndex;
    

    public PatrolState(Enemy enemy, NavMeshAgent navMeshAgent, Player player) : base(enemy, navMeshAgent, player)
    {
        
    }
    
    protected override void OnEnter()
    {
        _patrolPoints = Enemy.GetPatrolPoints().transforms;
        ReturnToTheFirstPatrolPoint();
    }

    private void ReturnToTheFirstPatrolPoint()
    {
        NavMeshAgent.destination = _patrolPoints[0].position;
        _currentDestinationIndex = 0;
    }

    public override void OnUpdate()
    {
        if (NavMeshAgent.remainingDistance<1)
        {
            _currentDestinationIndex++;
            if (Enemy.IsMovementRandom())
            {
                NavMeshAgent.destination = Enemy.GetRandomPatrolPoint();
            }
            else
            {
                NavMeshAgent.destination = _patrolPoints[_currentDestinationIndex % _patrolPoints.Length].position;
            }
            
        }
    }

    public override void OnExit()
    {
        
    }


}
