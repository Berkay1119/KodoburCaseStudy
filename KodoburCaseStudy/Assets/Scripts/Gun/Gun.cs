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
    [SerializeField] private GameSettings gameSettings;
    private int _ammoLevel=0;
    private int _damageLevel=0;
    [SerializeField] private bool isPierceActive;


    private void Start()
    {
        currentBullet = startingBullet;
        SetAmmoLevel(_ammoLevel);
        SetDamageLevel(_damageLevel);
        EventManager.OnAmmoUpdate(currentBullet,(float)currentBullet/maximumBullet);
    }

    private void OnEnable()
    {
        EventManager.Upgrade+=Upgrade;
    }

    private void OnDisable()
    {
        EventManager.Upgrade-=Upgrade;
    }

    private void Upgrade(Upgrades upgrades)
    {
        switch (upgrades)
        {
            case Upgrades.AmmoUpgrade:
                _ammoLevel++;
                SetAmmoLevel(_ammoLevel);
                break;
            case Upgrades.DamageUpgrade:
                _damageLevel++;
                SetDamageLevel(_damageLevel);
                break;
            case Upgrades.PierceShotUpgrade:
                isPierceActive = true;
                break;
        }
    }

    private void SetDamageLevel(int damageLevel)
    {
        attackDamage = gameSettings.damageAmountLevels[damageLevel];
        if (damageLevel==gameSettings.damageAmountLevels.Length-1)
        {
            EventManager.OnMaxUpgradeReached(Upgrades.DamageUpgrade);
        }
    }

    private void SetAmmoLevel(int ammoLevel)
    {
        maximumBullet = gameSettings.ammoCapacity[ammoLevel];
        if (ammoLevel==gameSettings.ammoCapacity.Length-1)
        {
            EventManager.OnMaxUpgradeReached(Upgrades.AmmoUpgrade);
        }
    }

    public void Shoot()
    {
        if (currentBullet == 0) { return; }
        currentBullet -= 1;
        EventManager.OnAmmoUpdate(currentBullet,(float)currentBullet/maximumBullet);
        Ray ray = playerCamera.ScreenPointToRay(new Vector3((float)Screen.width / 2, (float)Screen.height / 2, 0));
        if (Physics.Raycast(ray,out var hit))
        {
            print(hit.transform.name+" has been shot");
            if (hit.transform.TryGetComponent(out EnemyHitBox enemyHitBox))
            {
                enemyHitBox.TakeDamage(attackDamage);
                if (isPierceActive)
                {
                    if (Physics.Raycast(hit.transform.position,(hit.transform.position-transform.position).normalized, out var secondHit))
                    {
                        print(secondHit.transform.name + " has been shot");
                        if (secondHit.transform.TryGetComponent(out EnemyHitBox secondEnemyHitBox))
                        {
                            secondEnemyHitBox.TakeDamage(attackDamage);
                        }
                    }
                }
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
        int excessiveBullet = 0;
        if (currentBullet>maximumBullet)
        {
             excessiveBullet = currentBullet - maximumBullet;
            currentBullet = maximumBullet;
        }
        EventManager.OnAmmoUpdate(currentBullet,(float)currentBullet/maximumBullet);
        return excessiveBullet;
    }
}
