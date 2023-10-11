using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class AmmoIndicator : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMeshProUGUI;

    private void OnEnable()
    {
        EventManager.AmmoUpdate += Refresh;
    }

    private void OnDisable()
    {
        EventManager.AmmoUpdate -= Refresh;
    }

    private void Refresh(int ammo)
    {
        textMeshProUGUI.text = ammo.ToString();
    }
}
