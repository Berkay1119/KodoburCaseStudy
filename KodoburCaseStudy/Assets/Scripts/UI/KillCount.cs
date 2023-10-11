using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class KillCount : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMeshProUGUI;
    private int _killCount;

    private void Awake()
    {
        textMeshProUGUI.text = _killCount.ToString();
    }

    private void OnEnable()
    {
        EventManager.EnemyDied += IncreaseKillCount;
    }

    private void OnDisable()
    {
        EventManager.EnemyDied -= IncreaseKillCount;
    }

    private void IncreaseKillCount(Enemy enemy)
    {
        _killCount++;
        textMeshProUGUI.text = _killCount.ToString();
    }
}
