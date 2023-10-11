using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UpgradeTalent:MonoBehaviour
{
    [SerializeField] private int talentCost;
    [SerializeField] private Upgrades upgradeType;
    [SerializeField] private TextMeshProUGUI textMeshProUGUI;
    [SerializeField] private Button button;
    private bool _isMax;

    private void OnEnable()
    {
        EventManager.TalentGained+=Refresh;
        EventManager.MaxUpgradeReached += Close;
    }

    private void OnDisable()
    {
        EventManager.TalentGained-=Refresh;
        EventManager.MaxUpgradeReached -= Close;
    }

    private void Refresh(int talentPoint)
    {
        if (_isMax)
        {
            button.interactable = false;
            return;
        }

        button.interactable = talentPoint >= talentCost;
    }

    public void Upgrade()
    {
        EventManager.OnUpgrade(upgradeType);
    }
    
    private void Close(Upgrades upgrades)
    {
        if (upgrades==upgradeType)
        {
            button.interactable = false;
            _isMax = true;
        }
    }
    
}
