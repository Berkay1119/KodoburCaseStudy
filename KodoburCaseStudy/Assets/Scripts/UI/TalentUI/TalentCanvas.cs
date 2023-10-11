using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalentCanvas : MonoBehaviour
{
    [SerializeField] private GameObject characterUpgradeLayout;
    [SerializeField] private GameObject gunUpgradeLayout;
    [SerializeField] private GameObject backgroundCanvas;
    private int _talentPoint;

    private void OnEnable()
    {
        EventManager.RefreshTalentPoint += RefreshTalentPoint;
    }

    private void OnDisable()
    {
        EventManager.RefreshTalentPoint -= RefreshTalentPoint;
    }

    private void RefreshTalentPoint(int talentPoint)
    {
        _talentPoint = talentPoint;
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            characterUpgradeLayout.SetActive(!characterUpgradeLayout.activeInHierarchy);
            gunUpgradeLayout.SetActive(!gunUpgradeLayout.activeInHierarchy);
            backgroundCanvas.SetActive(!backgroundCanvas.activeInHierarchy);
            if (characterUpgradeLayout.activeInHierarchy)
            {
                EventManager.OnStopPlayerControl();
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                EventManager.OnStartPlayerControl();
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            EventManager.OnRefreshTalentPoint(_talentPoint);
        }
    }
}
