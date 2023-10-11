
using UnityEngine;
using UnityEngine.AI;

public class PatrolState : EnemyState
{
    private Transform[] _patrolPoints;
    private int _currentDestinationIndex;
    private static readonly int Blend = Animator.StringToHash("Blend");

    public PatrolState(Enemy enemy, NavMeshAgent navMeshAgent, Player player) : base(enemy, navMeshAgent, player)
    {
        
    }
    
    protected override void OnEnter()
    {
        _patrolPoints = Enemy.GetPatrolPoints().transforms;
        ReturnToTheFirstPatrolPoint();
        Enemy.GetAnimator().SetFloat(Blend,0.5f);
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
            NavMeshAgent.destination = Enemy.IsMovementRandom() ? Enemy.GetRandomPatrolPoint() : _patrolPoints[_currentDestinationIndex % _patrolPoints.Length].position;
        }
    }

    public override void OnExit()
    {
        
    }


}
