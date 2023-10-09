using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static event Action EnemyDied;
    public static event Action PlayerDied;

    public static void OnEnemyDied()
    {
        EnemyDied?.Invoke();
    }

    public static void OnPlayerDied()
    {
        PlayerDied?.Invoke();
    }
}
