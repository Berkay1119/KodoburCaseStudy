using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseRange : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;
    private Player _player;
    private void Awake()
    {
        _player = FindObjectOfType<Player>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (Physics.Raycast(_enemy.transform.position,_player.transform.position-_enemy.transform.position,out var hit))
        {
            if (hit.transform.TryGetComponent(out Player player))
            {
                _enemy.ChasePlayer(player);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (_enemy.GetCurrentState() is not ChaseState)
        {
            return;
        }
        if (other.TryGetComponent(out Player player))
        {
            _enemy.ReturnToYourPatrol();
        }
    }
}
