// Hunter McMahon
// 6/3/2024
// M - 6/3/2024
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerUpgradeController : MonoBehaviour
{   
    // accsssing player currency
    public WoodInventory Wallet;

    //UI stuff
    public GameObject upgradeMenu;
    public GameObject UpgradePrompt;

    // health upgrades/repairs
    public HealthController HC;
    private int HealthLvL = 0;
    public int HealthLVLCost = 25;
    public int dmgTaken = 0;
    public int RepairCost = 0;

    // movement speed
    public PlayerController PC
    private int speedLVL = 0;
    public int


    // cost textmesh
    public TextMeshProUGUI HealCostTxt;
    public TextMeshProUGUI HealthCostTxt;
    public TextMeshProUGUI DMGCostTxt;
    public TextMeshProUGUI MoveCostTxt;

    public void Heal()
    {
        if (HC.currentHealth < HC.initialHealth)
        {
            if (Wallet.WoodAmount >= RepairCost)
            {
                HC.Heal(dmgTaken);
                Wallet.SpendWood(RepairCost);
                dmgTaken = 0;
                RepairCost = 0;
                HealCostTxt.text = "Camp HP Full";
            }
        }
    }

    public void UpgradeHealth()
    {
        if (HealthLvL < 5)
        {
            if (Wallet.WoodAmount >= lvlCost)
            {
                Wallet.SpendWood(lvlCost);
                HC.IncMaxHP(25);
                HealthLvL ++;
                lvlCost += 15;
                HealthCostTxt.text = lvlCost + "Wood";
            }
            if (HealthLvL == 5)
            {
                HealthCostTxt.text = "Health Max'd";
            }
        }
    }

    public void UpgradeDMG()
    {}

    public void UpgradeMoveSPD()
    {

    }

    public void ExitMenu()
    {
        if(upgradeMenu.activeSelf)
        {
            SetActiveRecursively(upgradeMenu, false);
        }
    }

    public void SetActiveRecursively(GameObject obj, bool active)
    {
        obj.SetActive(active);

        foreach (Transform child in obj.transform)
        {
            SetActiveRecursively(child.gameObject, active);
        }
    }
}