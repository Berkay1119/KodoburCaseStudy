using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField] private int killCount;
    [SerializeField] private int talentPoints;
    [SerializeField] private Gun gun;

    private void Awake()
    {
        experiencePoint = 0;
        EventManager.OnRefreshHealthUI(1);
    }

    private void OnEnable()
    {
        EventManager.EnemyDied += EnemyKilled;
    }

    private void OnDisable()
    {
        EventManager.EnemyDied -= EnemyKilled;
    }

    private void EnemyKilled(Enemy enemy)
    {
        killCount++;
        experiencePoint += enemy.GetExperiencePoint();
        CalculateLevel();
    }

    private void CalculateLevel()
    {
        
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            gun.Shoot();
        }
    }
    protected override void Die()
    {
        EventManager.OnPlayerDied();
    }

    public void Heal(int healAmount)
    {
        currentHp += healAmount;
        currentHp = Mathf.Clamp(currentHp, 0, maxHp);
        EventManager.OnRefreshHealthUI((float)currentHp/maxHp);
    }

    public int AddBulletReturnExcessive(int contentAmount)
    {
        return gun.AddBulletReturnExcessive(contentAmount);
    }

    public override void TakeDamage(int damageAmount)
    {
        base.TakeDamage(damageAmount);
        EventManager.OnRefreshHealthUI((float)currentHp/maxHp);
    }
}
