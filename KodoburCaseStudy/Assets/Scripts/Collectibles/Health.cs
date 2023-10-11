using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : Collectible
{
    protected override void Collect(Player player)
    {
        EventManager.OnCollectibleCollected(this);
        player.Heal(contentAmount);
        ReturnToPool();
    }

    public override void Spawn(SpawnLocation spawnLocation)
    {
        base.Spawn(spawnLocation);
        SetAmount(gameSettings.healthAmountForEachCollectible);
    }

    public override void SetCooldown()
    {
        SpawnCooldown = gameSettings.spawnCooldownForHealth;
    }

    public override int MaxAmount()
    {
        return gameSettings.maxHealthAmount;
    }
}
