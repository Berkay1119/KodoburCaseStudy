using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject ammoGameObject;

    [SerializeField] private int poolAmount;

    [SerializeField] private GameSettings gameSettings;

    [SerializeField] private SpawnLocation[] candidateSpawnLocations;

    private List<ISpawnable> _pool = new List<ISpawnable>();


    private void Start()
    {
        CreatePool();
        InitialSetup();
    }

    private void OnEnable()
    {
        EventManager.CollectibleCollected += StartSpawnCooldown;
        EventManager.EnemyDied += StartSpawnCooldown;
    }

    private void OnDisable()
    {
        EventManager.CollectibleCollected -= StartSpawnCooldown;
        EventManager.EnemyDied -= StartSpawnCooldown;
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
        StartCoroutine(SpawnCooldown(spawnable));
    }

    private IEnumerator SpawnCooldown(ISpawnable spawnable)
    {
        yield return new WaitForSeconds(spawnable.SpawnCooldown);
        SpawnFrom(_pool);
    }
    

    private void SpawnFrom(List<ISpawnable> spawnables)
    {
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
    
    private void InitialSetup()
    {
        for (int i = 0; i < _pool[0].MaxAmount(); i++)
        {
            SpawnFrom(_pool);
        }
    }
}
