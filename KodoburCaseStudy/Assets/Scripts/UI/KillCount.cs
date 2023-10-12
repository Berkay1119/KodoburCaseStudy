using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class KillCount : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI killCountText;
    [SerializeField] private TextMeshProUGUI highestKillCountText;
    private int _killCount;
    private SaveSystem _saveSystem;
    private SaveData _saveData;
    private int _highestKillCount;

    private void Awake()
    {
        killCountText.text = _killCount.ToString();
        _saveSystem = new SaveSystem();
        _saveData=_saveSystem.LoadSaveData();
        _highestKillCount = _saveData.highestKillCount;
        highestKillCountText.text = _saveData.highestKillCount.ToString();
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
        killCountText.text = _killCount.ToString();
        if (_killCount>_saveData.highestKillCount)
        {
            _saveData.highestKillCount = _killCount;
            _saveSystem.SaveTheData(_saveData);
            _highestKillCount = _saveData.highestKillCount;
            highestKillCountText.text = _saveData.highestKillCount.ToString();
        }
    }
}
