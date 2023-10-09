using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;
    [SerializeField] private int startingBullet;
    [SerializeField] private int maximumBullet;
    private int _currentBullet;

    private void Start()
    {
        _currentBullet = startingBullet;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        if (_currentBullet == 0) { return; }
        _currentBullet -= 1;
        Ray ray = playerCamera.ScreenPointToRay(new Vector3((float)Screen.width / 2, (float)Screen.height / 2, 0));
        if (Physics.Raycast(ray,out var hit))
        {
            print(hit.transform.name+" has been shot");
        }
    }

    private int AddBulletReturnExcessive(int amount)
    {
        _currentBullet += amount;
        if (_currentBullet>maximumBullet)
        {
            int excessiveBullet = _currentBullet - maximumBullet;
            _currentBullet = maximumBullet;
            return excessiveBullet;
        }
        return 0;
    }
}
