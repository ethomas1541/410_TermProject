// Hunter McMahon
// 5/9/2024
// M - 5/10/2024
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CampUpgradeController : MonoBehaviour
{   
    // Events needed for the tutorial level
     public delegate void MenuCLosed();
     public event MenuCLosed OnExitMenu;
    //UI stuff
    public GameObject upgradeMenu;
    public GameObject UpgradePrompt;

    // accsssing player currency
    public WoodInventory Wallet;

    // health upgrades/repairs
    public HealthController HPcontroller;
    private int HealthLvL = 0;
    public int lvlCost = 25;
    public int dmgTaken = 0;
    public int RepairCost = 0;
    public TextMeshProUGUI RepairCostTxt;
    public TextMeshProUGUI UpgradeCostTxt;
    

    // the walls
    public GameObject Walls;
    public int WallsCost = 120;
    public TextMeshProUGUI WallsCostTXT;

    // Babe the Blue Bull
    public GameObject Babe;
    public TextMeshProUGUI BabeCostTxT;

    // poacher
    public TextMeshProUGUI PoacherCostTxT;

    void Start() 
    {
        // set upgrade objects to inactive
        Walls.SetActive(false);
        Babe.SetActive(false);
    }

    public void RepairCamp()
    {
        if (HPcontroller.currentHealth < HPcontroller.initialHealth)
        {
            if (Wallet.WoodAmount >= RepairCost)
            {
                HPcontroller.Heal(dmgTaken);
                Wallet.SpendWood(RepairCost);
                dmgTaken = 0;
                RepairCost = 0;
                RepairCostTxt.text = "Camp HP Full";
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
                HPcontroller.IncMaxHP(25);
                HealthLvL ++;
                lvlCost += 25;
                UpgradeCostTxt.text = lvlCost + "Wood";
            }
            if (HealthLvL == 5)
            {
                UpgradeCostTxt.text = "Camp At Max Level";
            }
        }
    }

    public void BuyOXPet()
    {
        if (Wallet.WoodAmount >= 200 && !(Babe.activeSelf))
        {
            Wallet.SpendWood(200);
            Babe.SetActive(true);
        }
    }

    public void Buildwall()
    {
        if (Wallet.WoodAmount >= WallsCost)
        {
            Wallet.SpendWood(WallsCost);
            SetActiveRecursively(Walls, false);
            SetActiveRecursively(Walls, true);
            WallsCost = 0;
            WallsCostTXT.text = "All Walls Built";
        }
    }

    public void HirePoacher()
    {
        // Implementation
        if (Wallet.WoodAmount >= 300)
        {
            Wallet.SpendWood(300);
            // poacher.SetActive(true);
            // TODO: find asset for poacher and set behavior
        }
    }

    public void ExitMenu()
    {
        upgradeMenu.SetActive(false);
        //UpgradePrompt.SetActive(true);
        OnExitMenu?.Invoke();
    }

    public static void SetActiveRecursively(GameObject obj, bool active)
    {
        obj.SetActive(active);

        foreach (Transform child in obj.transform)
        {
            SetActiveRecursively(child.gameObject, active);
        }
    }
}