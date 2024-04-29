// hp bar: https://www.youtube.com/watch?v=_lREXfAMUcE&t=4s&ab_channel=BMo
//
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoggingCamp : MonoBehaviour
{
    // depreciated, generic HP bar system being added
    public float MaxHP = 100;

    // hitpoints
    public float HP = 100;

    [SerializeField] HPBarHUD campHPbar;

    // Start is called before the first frame update
    void Start()
    {
        campHPbar.UpdateHPBar(HP, MaxHP);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(float DmgAmount)
    {
        HP -= DmgAmount;
        campHPbar.UpdateHPBar(HP, MaxHP);
        if (HP <= 0)
        {
            CampDestroyed();
        }
    }

    void CampDestroyed()
    {
        //todo: write code for lose condition when camp is destroyed
    }

    public void UpgradeCamp()
    {
        // todo: logic for buying camp upgrades
    }

    public void RepairCamp()
    {
        // todo: logic for repairing camp
    }
}
