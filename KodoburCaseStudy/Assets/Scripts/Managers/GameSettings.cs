
using UnityEngine;

[CreateAssetMenu(menuName = "Game Settings")]
public class GameSettings:ScriptableObject
{
    [Header("Enemy")]
    public int enemyShootCooldown;
    public float enemyAttackRadius;
}
