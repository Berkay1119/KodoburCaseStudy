
using UnityEngine.AI;

public class ChaseState : EnemyState
{
    private Player _player;
    public ChaseState(Enemy enemy, NavMeshAgent navMeshAgent, Player player) : base(enemy, navMeshAgent,player)
    {
        _player = player;
    }

    protected override void OnEnter()
    {
        
    }

    public override void OnUpdate()
    {
        NavMeshAgent.destination = _player.transform.position;
    }

    public override void OnExit()
    {
    }

    public void AssignPlayer(Player player)
    {
    }
}
