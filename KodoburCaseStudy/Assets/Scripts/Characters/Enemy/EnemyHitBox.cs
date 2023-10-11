
using UnityEngine;

public class EnemyHitBox:MonoBehaviour
{
    [SerializeField] private Enemy enemy;
    public void TakeDamage(int attackDamage)
    {
        enemy.TakeDamage(attackDamage);
    }
}
