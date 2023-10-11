using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject ammoGameObject;

    [SerializeField] private int poolAmount;

    [SerializeField] private GameSettings gameSettings;

    [SerializeField] private SpawnLocation[] candidateSpawnLocations;

    private List<ISpawnable> _pool = new List<ISpawnable>();
    
    [SerializeField] private SpawnerType spawnerType;


    private void Start()
    {
        CreatePool();
        InitialSetup();
    }

    private void OnEnable()
    {
        if (spawnerType==SpawnerType.Ammo || spawnerType == SpawnerType.Health)
        {
            EventManager.CollectibleCollected += StartSpawnCooldown;
        }
        
        if (spawnerType==SpawnerType.Enemy)
        {
            EventManager.EnemyDied += StartSpawnCooldown;
        }

        EventManager.PlayerDied += Stop;
    }

    private void Stop()
    {
        StopAllCoroutines();
    }

    private void OnDisable()
    {
        if (spawnerType==SpawnerType.Ammo || spawnerType == SpawnerType.Health)
        {
            EventManager.CollectibleCollected -= StartSpawnCooldown;
        }
        
        if (spawnerType==SpawnerType.Enemy)
        {
            EventManager.EnemyDied -= StartSpawnCooldown;
        }
        
        EventManager.PlayerDied -= Stop;
    }

    private void CreatePool()
    {
        for (int i = 0; i < poolAmount; i++)
        {
            ISpawnable spawnable=Instantiate(ammoGameObject, transform).GetComponent<ISpawnable>();
            spawnable.ReturnToPool();
            _pool.Add(spawnable);
        }

    }

    private void StartSpawnCooldown(ISpawnable spawnable)
    {
        if (spawnable is Ammo && spawnerType==SpawnerType.Ammo)
        {
            StartCoroutine(SpawnCooldown(spawnable));
        }
        else if (spawnable is Health && spawnerType==SpawnerType.Health)
        {
            StartCoroutine(SpawnCooldown(spawnable));
        }
        else if (spawnable is Enemy && spawnerType==SpawnerType.Enemy)
        {
            StartCoroutine(SpawnCooldown(spawnable));
        }
        
    }

    private IEnumerator SpawnCooldown(ISpawnable spawnable)
    {
        yield return new WaitForSeconds(spawnable.SpawnCooldown);
        SpawnFrom(_pool);
    }
    

    private void SpawnFrom(List<ISpawnable> spawnables)
    {
        ShuffleSpawnLocations();
        foreach (var spawnable in spawnables)
        {
            if (!spawnable.IsActive())
            {
                foreach (var spawnLocation in candidateSpawnLocations)
                {
                    if (spawnLocation.IsSpawnPointEmpty())
                    {
                        spawnable.Spawn(spawnLocation);
                        spawnLocation.MakeSpawnPointFull(true);
                        return;
                    }
                }
            }
        }
    }

    private void ShuffleSpawnLocations()
    {
        for (int i = 0; i < candidateSpawnLocations.Length; i++)
        {
            int j = Random.Range(0,candidateSpawnLocations.Length);
            (candidateSpawnLocations[i], candidateSpawnLocations[j]) = (candidateSpawnLocations[j], candidateSpawnLocations[i]);
        }
    }
    
    private void InitialSetup()
    {
        for (int i = 0; i < _pool[0].MaxAmount(); i++)
        {
            SpawnFrom(_pool);
        }
    }
}
