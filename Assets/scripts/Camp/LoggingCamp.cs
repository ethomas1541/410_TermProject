// hp bar: https://www.youtube.com/watch?v=_lREXfAMUcE&t=4s&ab_channel=BMo
// Hunter McMahon
// 5/10/2024
// M - 5/10/2024
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LoggingCamp : MonoBehaviour
{
    public HealthController healthController;
    //Animator animator;
    public CampUpgradeController CampUpgader;
    
    void Awake() {
        healthController = GetComponent<HealthController>();
        //animator = GetComponentInChildren<Animator>();

        // Subscribe to the OnDeath event
        healthController.OnDeath += OnDie;
        healthController.OnTakeDamage += OnDMG;
    }

    public void OnDie()
    {
        //todo: write code for lose condition when camp is destroyed
    }

    public void OnDMG()
    {
        CampUpgader.dmgTaken = healthController.initialHealth - healthController.currentHealth;
        CampUpgader.RepairCost = 2 * CampUpgader.dmgTaken;
        CampUpgader.RepairCostTxt.text =  CampUpgader.RepairCost + "Wood";
    }
}
