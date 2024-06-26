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
    public DamageTrigger DT;
    public TextMeshProUGUI BabeCostTxT;
    public TextMeshProUGUI BabeLabelTxT;
    private int BabeCost = 200;
    private int BabeLvL = 0;

    // poacher
    public TextMeshProUGUI PoacherCostTxT;
    public GameObject Poacher;

    // upgrade sounds
    public AudioSource aux;
    public AudioClip RepSFX;
    public AudioClip HealthUpgradeSFX;
    public AudioClip BabeSFX;
    public AudioClip Steroids;
    public AudioClip WallSFX;
    public AudioClip PoacherSFX;
    public AudioClip closeSFX;

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
                aux.PlayOneShot(RepSFX);
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
                HealthLvL++;
                lvlCost += 25;
                UpgradeCostTxt.text = lvlCost + "Wood";
                aux.PlayOneShot(HealthUpgradeSFX);
            }
            if (HealthLvL == 5)
            {
                UpgradeCostTxt.text = "Camp At Max Level";
            }
        }
    }

    public void BuyOXPet()
    {
        if (BabeLvL < 3)
        {
            if (Wallet.WoodAmount >= BabeCost)
            {
                if (!(Babe.activeSelf))
                {
                    Wallet.SpendWood(200);
                    Babe.SetActive(true);
                    aux.PlayOneShot(BabeSFX);
                    BabeCost = 40;
                    BabeCostTxT.text = "40 wood";
                    BabeLabelTxT.text = "Give Babe Steroids";
                }
                else
                {
                    BabeCost += 20;
                    BabeLvL++;
                    BabeCostTxT.text = BabeCost + " Wood";
                    DT.tagDamages[0].damage += 15;
                    aux.PlayOneShot(Steroids);
                }

                if (BabeLvL == 3)
                {
                    BabeCostTxT.text = "Babe Is Fully Juiced";
                }
            }
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
            aux.PlayOneShot(WallSFX);
        }
    }

    public void HirePoacher()
    {
        if (Wallet.WoodAmount >= 300 && !(Poacher.activeSelf))
        {
            Wallet.SpendWood(300);
            Poacher.SetActive(true);
            aux.PlayOneShot(PoacherSFX);
            PoacherCostTxT.text = "Poacher Hired";
        }
    }

    public void ExitMenu()
    {
        if (upgradeMenu.activeSelf)
        {
            aux.PlayOneShot(closeSFX);
            SetActiveRecursively(upgradeMenu, false);
            OnExitMenu?.Invoke();
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