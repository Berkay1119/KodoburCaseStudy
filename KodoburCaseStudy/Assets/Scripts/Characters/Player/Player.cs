using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField] private int killCount;
    [SerializeField] private int talentPoints;
    [SerializeField] protected Gun gun;

    private void Awake()
    {
        experiencePoint = 0;
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
        EventManager.OnRefreshUI();
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

    public Gun GetGun()
    {
        return gun;
    }

    public void Heal(int healAmount)
    {
        currentHp += healAmount;
        currentHp = Mathf.Clamp(currentHp, 0, maxHp);
    }
}
