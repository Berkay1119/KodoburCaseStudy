using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public delegate void EnemyEvent(Enemy enemy);
    public delegate void CollectibleEvent(Collectible spawnable);
    public delegate void FloatEvent(float integer);
    public static event EnemyEvent EnemyDied;
    public static event Action PlayerDied;
    public static event FloatEvent RefreshHealthUI;
    public static event CollectibleEvent CollectibleCollected;

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
}


