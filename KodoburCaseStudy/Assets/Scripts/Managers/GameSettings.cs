
using UnityEngine;

[CreateAssetMenu(menuName = "Game Settings")]
public class GameSettings:ScriptableObject
{
    [Header("Enemy")]
    public int enemyShootCooldown;
    public float enemyAttackRadius;
    
    [Header("Collectibles")]
    public int ammoAmountForEachCollectible=3;
    public int healthAmountForEachCollectible=20;
    public float spawnCooldownForAmmo;
    public float spawnCooldownForHealth;
    public int maxAmmoAmount=3;
    public int maxHealthAmount=1;
}
