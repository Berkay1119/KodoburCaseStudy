using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthCanvas : MonoBehaviour
{
    [SerializeField] private Slider slider;
    private Player _player;

    private void Update()
    {
        transform.LookAt(_player.transform);
    }

    public void RefreshHealth(float currentHpRatio)
    {
        slider.value = currentHpRatio;
    }

    public void SetPlayer(Player player)
    {
        _player = player;
    }
}
