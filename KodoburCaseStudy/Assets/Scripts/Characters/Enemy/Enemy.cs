using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Character, ISpawnable
{
    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] private int attackDamage;
    [SerializeField] private TrailRenderer bulletPrefab;
    private EnemyState _state;
    private Player _player;
    private EnemyPatrolManager _patrolManager;
    [SerializeField] private EnemyHealthCanvas enemyHealthCanvas;

    private void Awake()
    {
        _patrolManager = FindObjectOfType<EnemyPatrolManager>();
        _player = FindObjectOfType<Player>();
        SetCooldown();
        enemyHealthCanvas.SetPlayer(_player);
        enemyHealthCanvas.RefreshHealth(1);
    }

    private void OnEnable()
    {
        EventManager.PlayerDied += Stop;
    }

    private void OnDisable()
    {
        EventManager.PlayerDied -= Stop;
    }

    private void Stop()
    {
        _state = null;
        StopAllCoroutines();
    }

    private void Update()
    {
        _state?.OnUpdate();
    }

    protected override void Die()
    {
        EventManager.OnEnemyDied(this);
        ReturnToPool();
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
            TrailRenderer bullet=Instantiate(bulletPrefab);
            var position = transform.position;
            bullet.AddPosition(position);
            {
                bullet.transform.position = (_player.transform.position);
            }
            _player.TakeDamage(attackDamage);
            yield return new WaitForSeconds(gameSettings.enemyShootCooldown);
        }
    }

    //TODO: EnemySpawner should give PatrolPoints to the enemy.
    public PatrolPoints GetPatrolPoints()
    {
        return _patrolManager.GetPatrolPoints();
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
        currentHp = maxHp;
        transform.position = spawnLocation.transform.position;
        SpawnLocation= spawnLocation;
        enemyHealthCanvas.RefreshHealth(1);
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

    public bool IsMovementRandom()
    {
        return gameSettings.isEnemyMovementRandom;
    }

    public Vector3 GetRandomPatrolPoint()
    {
        return _patrolManager.GetRandomPatrolPoint().position;
    }

    public override void TakeDamage(int damageAmount)
    {
        base.TakeDamage(damageAmount);
        enemyHealthCanvas.RefreshHealth((float)currentHp / maxHp);
    }
}
