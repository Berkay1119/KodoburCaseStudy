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
}
