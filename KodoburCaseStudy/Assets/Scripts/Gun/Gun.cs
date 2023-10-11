using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Gun : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;
    [SerializeField] private int startingBullet;
    [SerializeField] private int maximumBullet;
    [SerializeField] private int attackDamage=10;
    [SerializeField] private int currentBullet;
    [SerializeField] private TrailRenderer bulletPrefab;
    [SerializeField] private Transform gunTip;

    private void Start()
    {
        currentBullet = startingBullet;
    }
    

    public void Shoot()
    {
        
        if (currentBullet == 0) { return; }
        currentBullet -= 1;
        
        Ray ray = playerCamera.ScreenPointToRay(new Vector3((float)Screen.width / 2, (float)Screen.height / 2, 0));
        if (Physics.Raycast(ray,out var hit))
        {
            print(hit.transform.name+" has been shot");
            if (hit.transform.TryGetComponent(out EnemyHitBox enemyHitBox))
            {
                enemyHitBox.TakeDamage(attackDamage);
            }
        }
        
        TrailRenderer bullet=Instantiate(bulletPrefab);
        bullet.AddPosition(gunTip.position);
        {
            bullet.transform.position = gunTip.position + (playerCamera.transform.forward * 200);
        }
        
    }

    public int AddBulletReturnExcessive(int amount)
    {
        currentBullet += amount;
        if (currentBullet>maximumBullet)
        {
            int excessiveBullet = currentBullet - maximumBullet;
            currentBullet = maximumBullet;
            return excessiveBullet;
        }
        return 0;
    }
}
