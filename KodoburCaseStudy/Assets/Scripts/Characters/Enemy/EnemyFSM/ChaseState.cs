
using UnityEngine;
using UnityEngine.AI;

public class ChaseState : EnemyState
{
    private static readonly int Blend = Animator.StringToHash("Blend");

    public ChaseState(Enemy enemy, NavMeshAgent navMeshAgent, Player player) : base(enemy, navMeshAgent,player)
    {
    }

    protected override void OnEnter()
    {
        Enemy.GetAnimator().SetFloat(Blend,1f);
    }

    public override void OnUpdate()
    {
        var transform = Player.transform;
        var position = transform.position;
        NavMeshAgent.destination = position;
        Enemy.transform.LookAt(new Vector3(position.x,Enemy.transform.position.y,position.z));
    }

    public override void OnExit()
    {
    }

}
