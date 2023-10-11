using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class LevelIndicatorDisplay : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI tmpArea;

    private void OnEnable()
    {
        EventManager.LevelUpdate+=Refresh;
    }

    private void OnDisable()
    {
        EventManager.LevelUpdate-=Refresh;
    }

    private void Refresh(float levelRatio, int levelCount)
    {
        tmpArea.text = levelCount.ToString();
        slider.value = levelRatio;
    }
}
