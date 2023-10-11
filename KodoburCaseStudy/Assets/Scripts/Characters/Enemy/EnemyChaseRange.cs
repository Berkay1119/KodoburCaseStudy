using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemyChaseRange : MonoBehaviour
{
    [SerializeField] private Enemy enemy;
    private Player _player;
    private void Awake()
    {
        _player = FindObjectOfType<Player>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (Physics.Raycast(enemy.transform.position,_player.transform.position-enemy.transform.position,out var hit))
        {
            if (hit.transform.TryGetComponent(out Player player))
            {
                if (enemy.GetCurrentState() is PatrolState)
                {
                    enemy.ChasePlayer();
                }
                
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (enemy.GetCurrentState() is not ChaseState)
        {
            return;
        }
        if (other.TryGetComponent(out Player player))
        {
            enemy.ReturnToYourPatrol();
        }
    }
}
