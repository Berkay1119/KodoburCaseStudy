using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class AmmoIndicator : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMeshProUGUI;
    [SerializeField] private Image image;

    private void OnEnable()
    {
        EventManager.AmmoUpdate += Refresh;
    }

    private void OnDisable()
    {
        EventManager.AmmoUpdate -= Refresh;
    }

    private void Refresh(int ammo, float ratio)
    {
        textMeshProUGUI.text = ammo.ToString();
        image.fillAmount = ratio;
    }
}
