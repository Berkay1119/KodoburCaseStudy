using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackRange : MonoBehaviour
{
    [SerializeField] private Enemy enemy;
    private Coroutine _shootRoutine;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            _shootRoutine = StartCoroutine(enemy.ShootRoutine(player));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        StopCoroutine(_shootRoutine);
    }
}
