using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public delegate void EnemyEvent(Enemy enemy);
    public delegate void CollectibleEvent(Collectible spawnable);
    public static event EnemyEvent EnemyDied;
    public static event Action PlayerDied;
    public static event Action RefreshUI;
    public static event CollectibleEvent CollectibleCollected;

    public static void OnEnemyDied(Enemy enemy)
    {
        EnemyDied?.Invoke(enemy);
    }

    public static void OnPlayerDied()
    {
        PlayerDied?.Invoke();
    }

    public static void OnRefreshUI()
    {
        RefreshUI?.Invoke();
    }

    public static void OnCollectibleCollected(Collectible spawnable)
    {
        CollectibleCollected?.Invoke(spawnable);
    }
}


