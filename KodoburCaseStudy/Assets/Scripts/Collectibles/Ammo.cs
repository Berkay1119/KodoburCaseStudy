using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : Collectible
{
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
            contentAmount = excessiveAmmo;
        }
    }
}
