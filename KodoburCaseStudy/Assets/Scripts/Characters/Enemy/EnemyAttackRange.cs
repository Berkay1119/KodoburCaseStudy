using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemyAttackRange : MonoBehaviour
{
    [SerializeField] private Enemy enemy;
    [SerializeField] private SphereCollider attackCollider;
    [SerializeField] private GameSettings gameSettings;
    private Coroutine _shootRoutine;

    private void Awake()
    {
        AdjustCollider(gameSettings.enemyAttackRadius);
    }

    private void AdjustCollider(float radius)
    {
        attackCollider.radius = radius;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            if (enemy.GetCurrentState() is ChaseState)
            {
                if (Physics.Raycast(enemy.transform.position,player.transform.position-enemy.transform.position,out var hit))
                {
                    if (hit.transform.TryGetComponent(out Player localPlayer))
                    {
                        enemy.Shoot();
                    }
                }
                
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
