using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : Collectible
{
    public override void Spawn(SpawnLocation spawnLocation)
    {
        base.Spawn(spawnLocation);
        SetAmount(gameSettings.ammoAmountForEachCollectible);
    }

    public override void SetCooldown()
    {
        SpawnCooldown = gameSettings.spawnCooldownForAmmo;
    }

    public override int MaxAmount()
    {
        return gameSettings.maxAmmoAmount;
    }

    protected override void Collect(Player player)
    {
        EventManager.OnCollectibleCollected(this);
        int excessiveAmmo = player.AddBulletReturnExcessive(contentAmount);

        if (excessiveAmmo==0)
        {
            ReturnToPool();
        }
        else
        {
            SetAmount(excessiveAmmo);
        }
    }
}
