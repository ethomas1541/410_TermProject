using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampUpgradeController : MonoBehaviour
{   
    //UI stuff
    public GameObject upgradeMenu;
    public GameObject UpgradePrompt;

    // health upgrades/repairs
    public GameObject CampOBJ;
    private HealthController HPcontroller;
    private int HealthLvL = 0;

    // the walls
    public GameObject Walls;

    // Babe the Blue Bull
    public GameObject Babe;

    void Start() 
    {
        HPcontroller = CampOBJ.GetComponent<HealthController>();
        Walls.SetActive(false);
        Babe.SetActive(false);
    }

    public void RepairCamp()
    {
        if (HPcontroller.currentHealth < HPcontroller.initialHealth)
        {
            HPcontroller.Heal(HPcontroller.initialHealth - HPcontroller.currentHealth);
        }
    }

    public void UpgradeHealth()
    {
        if (HealthLvL < 5)
        {
            HPcontroller.IncMaxHP(25);
            HealthLvL ++;
        }
    }

    public void BuyOXPet()
    {
        Babe.SetActive(true);
    }

    public void Buildwall()
    {
        Walls.SetActive(false);
        Walls.SetActive(true);
    }

    public void HirePoacher()
    {
        // Implementation
    }

    public void ExitMenu()
    {
        upgradeMenu.SetActive(false);
        UpgradePrompt.SetActive(true);
    }
}