using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Collectible : MonoBehaviour
{
    [SerializeField] protected int contentAmount;
    private SpawnLocation _spawnLocation;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            Collect(player);
        }
    }

    protected abstract void Collect(Player player);

    protected void ReturnToPool()
    { 
        gameObject.SetActive(false);
        _spawnLocation.MakeSpawnPointFull(false);
    }

    public void SetAmount(int amount)
    {
        contentAmount = amount;
    }

    public void Spawn(SpawnLocation spawnLocation)
    {
        transform.position = spawnLocation.transform.position;
        _spawnLocation = spawnLocation;
        gameObject.SetActive(true);
    }

}
