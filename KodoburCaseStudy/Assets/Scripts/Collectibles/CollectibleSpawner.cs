using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CollectibleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject ammoGameObject;
    [SerializeField] private GameObject healthGameObject;

    [SerializeField] private int poolAmountForAmmo;
    [SerializeField] private int poolAmountForHealth;

    [SerializeField] private GameSettings gameSettings;

    [SerializeField] private SpawnLocation[] candidateSpawnLocations;

    private List<Collectible> _ammoPool = new List<Collectible>();
    private List<Collectible> _healthPool = new List<Collectible>();

    private void Awake()
    {
        CreatePool();
        InitialSetup();
    }

    private void OnEnable()
    {
        EventManager.CollectibleCollected += StartSpawnCooldown;
    }

    private void OnDisable()
    {
        EventManager.CollectibleCollected -= StartSpawnCooldown;
    }

    private void CreatePool()
    {
        for (int i = 0; i < poolAmountForAmmo; i++)
        {
            Ammo ammo=Instantiate(ammoGameObject, transform).GetComponent<Ammo>();
            ammo.SetAmount(gameSettings.ammoAmountForEachCollectible);
            ammo.gameObject.SetActive(false);
            _ammoPool.Add(ammo);
        }
        for (int i = 0; i < poolAmountForHealth; i++)
        {
            Health health=Instantiate(healthGameObject, transform).GetComponent<Health>();
            health.SetAmount(gameSettings.healthAmountForEachCollectible);
            health.gameObject.SetActive(false);
            _healthPool.Add(health);
        }
    }

    private void StartSpawnCooldown(Collectible collectible)
    {
        StartCoroutine(SpawnCooldown(collectible));
    }

    private IEnumerator SpawnCooldown(Collectible collectible)
    {
        if (collectible is Ammo)
        {
            yield return new WaitForSeconds(gameSettings.spawnCooldownForAmmo);
            SpawnFrom(_ammoPool);
        }
        else if(collectible is Health) 
        {
            yield return new WaitForSeconds(gameSettings.spawnCooldownForHealth);
            SpawnFrom(_healthPool);
        }
    }
    

    private void SpawnFrom(List<Collectible> collectibles)
    {
        foreach (var collectible in collectibles)
        {
            if (!collectible.isActiveAndEnabled)
            {
                foreach (var spawnLocation in candidateSpawnLocations)
                {
                    if (spawnLocation.IsSpawnPointEmpty())
                    {
                        collectible.Spawn(spawnLocation);
                        spawnLocation.MakeSpawnPointFull(true);
                        if(collectible is Ammo)
                        {
                            collectible.SetAmount(gameSettings.ammoAmountForEachCollectible);
                        }
                        else
                        {
                            collectible.SetAmount(gameSettings.healthAmountForEachCollectible);
                        }
                        
                        return;
                    }
                }
            }
        }
    }
    
    private void InitialSetup()
    {
        for (int i = 0; i < gameSettings.maxAmmoAmount; i++)
        {
            SpawnFrom(_ammoPool);
        }
        for (int i = 0; i < gameSettings.maxHealthAmount; i++)
        {
            SpawnFrom(_healthPool);
        }
    }
}
