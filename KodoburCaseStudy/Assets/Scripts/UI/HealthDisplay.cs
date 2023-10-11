using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    [SerializeField] private Slider slider;

    private void OnEnable()
    {
        EventManager.RefreshHealthUI += RefreshUI;
    }

    private void OnDisable()
    {
        EventManager.RefreshHealthUI -= RefreshUI;
    }

    private void RefreshUI(float value)
    {
        slider.value = value;
    }
}
