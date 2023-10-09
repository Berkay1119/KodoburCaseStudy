using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField] private int killCount;

    private void OnEnable()
    {
        EventManager.EnemyDied += IncreaseKillCount;
    }

    private void OnDisable()
    {
        EventManager.EnemyDied -= IncreaseKillCount;
    }

    private void IncreaseKillCount()
    {
        killCount++;
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
}
