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
    public int HealCost = 0;

    // movement speed
    public PlayerController PC;
    private int speedLVL = 0;
    public int SpeedLVLCost = 25;

    // Damage 
    public DamageTrigger DT;
    private int DMGLVL = 0;
    public int DMGLVLCost = 40;


    // cost textmesh
    public TextMeshProUGUI HealCostTxt;
    public TextMeshProUGUI HealthCostTxt;
    public TextMeshProUGUI DMGCostTxt;
    public TextMeshProUGUI MoveCostTxt;

    // upgrade sounds

    void Start()
    {
        HC.OnTakeDamage += OnDMG;
    }
    public void OnDMG()
    {
        dmgTaken = HC.initialHealth - HC.currentHealth;
        HealCost = dmgTaken;
        HealCostTxt.text = HealCost + "Wood";
    }

    public void Heal()
    {
        if (HC.currentHealth < HC.initialHealth)
        {
            if (Wallet.WoodAmount >= HealCost)
            {
                HC.Heal(dmgTaken);
                Wallet.SpendWood(HealCost);
                dmgTaken = 0;
                HealCost = 0;
                HealCostTxt.text = "HP Full";
            }
        }
    }

    public void UpgradeHealth()
    {
        if (HealthLvL < 5)
        {
            if (Wallet.WoodAmount >= HealthLVLCost)
            {
                Wallet.SpendWood(HealthLVLCost);
                HC.IncMaxHP(25);
                HealthLvL++;
                HealthLVLCost += 15;
                HealthCostTxt.text = HealthLVLCost + "Wood";
            }
            if (HealthLvL == 5)
            {
                HealthCostTxt.text = "Health Max'd";
            }
        }
    }

    public void UpgradeDMG()
    {
        if (DMGLVL < 7)
        {
            if (Wallet.WoodAmount >= DMGLVLCost)
            {
                Wallet.SpendWood(DMGLVLCost);
                DT.tagDamages[0].damage += 10;
                DT.tagDamages[1].damage += 10;
                DMGLVL++;
                DMGLVLCost += 20;
                DMGCostTxt.text = DMGLVLCost + "Wood";
            }
            if (DMGLVL == 7)
            {
                DMGCostTxt.text = "Damage Max'd";
            }
        }
    }

    public void UpgradeMoveSPD()
    {
        if (speedLVL < 3)
        {
            if (Wallet.WoodAmount >= SpeedLVLCost)
            {
                Wallet.SpendWood(SpeedLVLCost);
                PC.speed += 1.25f;
                speedLVL++;
                SpeedLVLCost += 10;
                MoveCostTxt.text = SpeedLVLCost + "Wood";
            }
            if (speedLVL == 3)
            {
                MoveCostTxt.text = "Speed Max'd";
            }
        }
    }

    public void ExitMenu()
    {
        if (upgradeMenu.activeSelf)
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