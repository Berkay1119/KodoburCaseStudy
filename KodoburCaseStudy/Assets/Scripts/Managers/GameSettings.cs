
using UnityEngine;
using UnityEngine.Serialization;

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
    public float enemySpawnCooldown=10;
    public int maxEnemyAmount = 5;
    public bool isEnemyMovementRandom;

    [Header("Level Settings")] 
    public int[] levelPassXp;
    
}
