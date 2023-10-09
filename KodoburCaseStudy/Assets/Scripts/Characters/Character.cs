using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class Character : MonoBehaviour
{
    [SerializeField] protected int experiencePoint;
    [SerializeField] protected int maxHp;
    [SerializeField] protected int currentHp=100;
    [SerializeField] protected GameSettings gameSettings;

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
