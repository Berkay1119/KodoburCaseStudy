using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField] private int killCount;
    [SerializeField] private int talentPoints;
    [SerializeField] private Gun gun;
    private int _level=1;
    private bool _isStopped;
    private int _healthLevel;

    private void Awake()
    {
        maxHp = gameSettings.maxHealthLevels[_healthLevel];
        currentHp = maxHp;
        experiencePoint = 0;
        EventManager.OnRefreshHealthUI(1f);
    }

    private void OnEnable()
    {
        EventManager.EnemyDied += EnemyKilled;
        EventManager.Upgrade += Upgrade;
        EventManager.StopPlayerControl += Stop;
        EventManager.StartPlayerControl += StartPlayerControl;
    }
    private void OnDisable()
    {
        EventManager.EnemyDied -= EnemyKilled;
        EventManager.Upgrade -= Upgrade;
        EventManager.StopPlayerControl -= Stop;
        EventManager.StartPlayerControl -= StartPlayerControl;
    }

    private void StartPlayerControl()
    {
        _isStopped = false;
    }

    private void Stop()
    {
        _isStopped = true;
    }

    private void Upgrade(Upgrades upgrades)
    {
        talentPoints--;
        if (upgrades==Upgrades.HealthUpgrade)
        {
            _healthLevel++;
            maxHp = gameSettings.maxHealthLevels[_healthLevel];
            if (gameSettings.maxHealthLevels.Length-1==_healthLevel)
            {
                EventManager.OnMaxUpgradeReached(Upgrades.HealthUpgrade);
            }
        }
        EventManager.OnRefreshTalentPoint(talentPoints);
    }



    private void EnemyKilled(Enemy enemy)
    {
        killCount++;
        experiencePoint += enemy.GetExperiencePoint();
        CalculateLevel();
    }

    private void CalculateLevel()
    {
        if (gameSettings.levelPassXp[_level-1]<=experiencePoint)
        {
            if (_level!=gameSettings.levelPassXp.Length)
            {
                experiencePoint = 0;
            }
            _level++;
            talentPoints++;
            EventManager.OnRefreshTalentPoint(talentPoints);
            _level = Mathf.Clamp(_level,0, gameSettings.levelPassXp.Length);
        }
        EventManager.OnLevelUpdate((float)experiencePoint/gameSettings.levelPassXp[_level-1],_level);
    }

    private void Update()
    {
        if (_isStopped)
        {
            return;
        }
        if (Input.GetButtonDown("Fire1"))
        {
            gun.Shoot();
        }
    }
    protected override void Die()
    {
        _isStopped = true;
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
