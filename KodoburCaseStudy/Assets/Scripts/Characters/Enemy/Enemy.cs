using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    [SerializeField] private int attackDamage;
    [SerializeField] private EnemyAttackRange enemyAttackRange;

    private void Awake()
    {
        enemyAttackRange.AdjustCollider(gameSettings.enemyAttackRadius);
    }

    protected override void Die()
    {
        EventManager.OnEnemyDied(this);
    }

    public int GetExperiencePoint()
    {
        return experiencePoint;
    }

    public IEnumerator ShootRoutine(Player player)
    {
        while (true)
        {
            player.TakeDamage(attackDamage);
            yield return new WaitForSeconds(gameSettings.enemyShootCooldown);
        }
    }
}
