using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class Character : MonoBehaviour
{
    [SerializeField] private int maxHp;
    [SerializeField] private int currentHp=100;
    [SerializeField] protected Gun gun;

    public void TakeDamage(int damageAmount)
    {
        currentHp -= damageAmount;
        if (currentHp<=0)
        {
            Die();
        }
    }

    protected abstract void Die();

}
