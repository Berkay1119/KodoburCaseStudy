
using UnityEngine.AI;

public class ChaseState : EnemyState
{
    public ChaseState(Enemy enemy, NavMeshAgent navMeshAgent, Player player) : base(enemy, navMeshAgent,player)
    {
    }

    protected override void OnEnter()
    {
        
    }

    public override void OnUpdate()
    {
        NavMeshAgent.destination = Player.transform.position;
    }

    public override void OnExit()
    {
    }

}
