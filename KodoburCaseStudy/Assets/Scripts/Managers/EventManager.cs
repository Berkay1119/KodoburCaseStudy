using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public delegate void EnemyEvent(Enemy enemy);
    public delegate void CollectibleEvent(Collectible spawnable);
    public delegate void FloatEvent(float floatNumber);
    public delegate void IntEvent(int integer);
    public delegate void LevelEvent(float levelRatio,int levelCount);
    public delegate void UpgradeEvent(Upgrades upgrades);
    public static event EnemyEvent EnemyDied;
    public static event Action PlayerDied;
    public static event FloatEvent RefreshHealthUI;
    public static event CollectibleEvent CollectibleCollected;
    public static event LevelEvent LevelUpdate;
    public static event IntEvent AmmoUpdate;
    public static event UpgradeEvent Upgrade;
    public static event UpgradeEvent MaxUpgradeReached;
    public static event IntEvent RefreshTalentPoint;
    public static event Action StopPlayerControl;
    public static event Action StartPlayerControl;

    public static void OnEnemyDied(Enemy enemy)
    {
        EnemyDied?.Invoke(enemy);
    }

    public static void OnPlayerDied()
    {
        PlayerDied?.Invoke();
    }

    public static void OnRefreshHealthUI(float x)
    {
        RefreshHealthUI?.Invoke(x);
    }

    public static void OnCollectibleCollected(Collectible spawnable)
    {
        CollectibleCollected?.Invoke(spawnable);
    }

    public static void OnLevelUpdate(float levelRatio, int levelCount)
    {
        LevelUpdate?.Invoke(levelRatio, levelCount);
    }

    public static void OnAmmoUpdate(int integer)
    {
        AmmoUpdate?.Invoke(integer);
    }

    public static void OnUpgrade(Upgrades upgrades)
    {
        Upgrade?.Invoke(upgrades);
    }

    public static void OnMaxUpgradeReached(Upgrades upgrades)
    {
        MaxUpgradeReached?.Invoke(upgrades);
    }

    public static void OnStopPlayerControl()
    {
        StopPlayerControl?.Invoke();
    }

    public static void OnStartPlayerControl()
    {
        StartPlayerControl?.Invoke();
    }

    public static void OnRefreshTalentPoint(int integer)
    {
        RefreshTalentPoint?.Invoke(integer);
    }
}


