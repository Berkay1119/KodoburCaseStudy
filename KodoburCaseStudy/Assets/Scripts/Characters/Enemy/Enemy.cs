using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Character, ISpawnable
{
    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] private Vector3[]patrolPoints;
    [SerializeField] private int attackDamage;
    private EnemyState _state;
    private Player _player;

    private void Awake()
    {
        _player = FindObjectOfType<Player>();
        SetCooldown();
    }

    private void Update()
    {
        _state?.OnUpdate();
    }

    protected override void Die()
    {
        EventManager.OnEnemyDied(this);
    }

    public int GetExperiencePoint()
    {
        return experiencePoint;
    }

    public void Shoot()
    {
        ChangeState(new ShootingState(this,navMeshAgent,_player));
    }
    
    public IEnumerator ShootRoutine()
    {
        while (true)
        {
            _player.TakeDamage(attackDamage);
            yield return new WaitForSeconds(gameSettings.enemyShootCooldown);
        }
    }

    //TODO: EnemySpawner should give PatrolPoints to the enemy.
    public Vector3[] GetPatrolPoints()
    {
        return patrolPoints;
    }

    public void ChasePlayer()
    {
        ChangeState(new ChaseState(this,navMeshAgent,_player));
    }
    
    public void ReturnToYourPatrol()
    {
        ChangeState(new PatrolState(this,navMeshAgent,_player));
    }

    private void ChangeState(EnemyState newState)
    {
        _state?.OnExit();
        _state = newState;
    }


    public EnemyState GetCurrentState()
    {
        return _state;
    }

    public SpawnLocation SpawnLocation { get; set; }
    public float SpawnCooldown { get; set; }
    public void ReturnToPool()
    {
        gameObject.SetActive(false);
        if (SpawnLocation!=null)
        {
            SpawnLocation.MakeSpawnPointFull(false);
        }
        _state = null;
    }

    public void Spawn(SpawnLocation spawnLocation)
    {
        transform.position = spawnLocation.transform.position;
        SpawnLocation= spawnLocation;
        gameObject.SetActive(true);
        _state = new PatrolState(this, navMeshAgent, _player);
    }

    public void SetCooldown()
    {
        SpawnCooldown = gameSettings.enemySpawnCooldown;
    }

    public bool IsActive()
    {
        return gameObject.activeInHierarchy;
    }

    public int MaxAmount()
    {
        return gameSettings.maxEnemyAmount;
    }
}
