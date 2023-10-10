using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackRange : MonoBehaviour
{
    [SerializeField] private Enemy enemy;
    [SerializeField] private SphereCollider attackCollider;
    private Coroutine _shootRoutine;

    public void AdjustCollider(float radius)
    {
        attackCollider.radius = radius;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            if (enemy.GetCurrentState() is ChaseState)
            {
                enemy.Shoot(player);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            if (enemy.GetCurrentState() is ShootingState)
            {
                enemy.ChasePlayer();
            }
        }
    }
}
