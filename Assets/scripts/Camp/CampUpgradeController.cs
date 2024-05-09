using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampUpgradeController : MonoBehaviour
{   
    //UI stuff
    public GameObject upgradeMenu;
    public GameObject UpgradePrompt;

    // accsssing player currency
    public GameObject Player;
    private WoodInventory Wallet;

    // health upgrades/repairs
    public GameObject CampOBJ;
    private HealthController HPcontroller;
    private int HealthLvL = 0;
    public int lvlCost = 25;

    // the walls
    public GameObject Walls;

    // Babe the Blue Bull
    public GameObject Babe;

    void Start() 
    {
        // get info from other classes that we need here
        HPcontroller = CampOBJ.GetComponent<HealthController>();
        Wallet = Player.GetComponent<WoodInventory>();
        // set upgrade objects to inactive
        Walls.SetActive(false);
        Babe.SetActive(false);
    }

    public void RepairCamp()
    {
        if (HPcontroller.currentHealth < HPcontroller.initialHealth)
        {
            int dmgTaken = HPcontroller.initialHealth - HPcontroller.currentHealth;
            int RepairCost = 2 * dmgTaken;
            if (Wallet.WoodAmount >= RepairCost)
            {
                HPcontroller.Heal(dmgTaken);
                Wallet.SpendWood(RepairCost);
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
            }
        }
    }

    public void BuyOXPet()
    {
        if (Wallet.WoodAmount >= 200)
        {
            Wallet.SpendWood(200);
            Babe.SetActive(true);
        }
    }

    public void Buildwall()
    {
        if (Wallet.WoodAmount >= 120)
        {
            Wallet.SpendWood(120);
            Walls.SetActive(false);
            Walls.SetActive(true);
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
        UpgradePrompt.SetActive(true);
    }
}