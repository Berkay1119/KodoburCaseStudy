using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class Collectible : MonoBehaviour,ISpawnable
{
    [SerializeField] protected int contentAmount;
    [SerializeField] protected GameSettings gameSettings;

    public SpawnLocation SpawnLocation { get; set; }
    public float SpawnCooldown { get; set; }


    private void Awake()
    {
        SetCooldown();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            Collect(player);
        }
    }

    protected abstract void Collect(Player player);

    public void SetAmount(int amount)
    {
        contentAmount = amount;
    }
    
    public void ReturnToPool()
    {
        gameObject.SetActive(false);
        if (SpawnLocation!=null)
        {
            SpawnLocation.MakeSpawnPointFull(false);
        }
    }

    public virtual void Spawn(SpawnLocation spawnLocation)
    {
        transform.position = spawnLocation.transform.position;
        SpawnLocation = spawnLocation;
        gameObject.SetActive(true);
    }
    public abstract void SetCooldown();
    public bool IsActive()
    {
        return gameObject.activeInHierarchy;
    }

    public abstract int MaxAmount();

}
