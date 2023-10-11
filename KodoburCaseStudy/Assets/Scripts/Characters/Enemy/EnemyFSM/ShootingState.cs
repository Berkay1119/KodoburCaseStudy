using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;

public class ShootingState: EnemyState
{
    private Coroutine _shootRoutine;
    private static readonly int Shoot = Animator.StringToHash("Shoot");

    public ShootingState(global::Enemy enemy, NavMeshAgent navMeshAgent,Player player) : base(enemy, navMeshAgent, player)
    {
    }

    protected override void OnEnter()
    {
        _shootRoutine = Enemy.StartCoroutine(Enemy.ShootRoutine());
        NavMeshAgent.isStopped = true;
        Enemy.GetAnimator().SetBool(Shoot,true);
    }

    public override void OnUpdate()
    {
        var position = Player.transform.position;
        Enemy.transform.LookAt(new Vector3(position.x,Enemy.transform.position.y,position.z));
    }

    public override void OnExit()
    {
        if (Enemy.GetCurrentState() is ShootingState)
        {
            Enemy.StopCoroutine(_shootRoutine);
            NavMeshAgent.isStopped = false;
        }
        Enemy.GetAnimator().SetBool(Shoot,false);
        
    }
    
}
