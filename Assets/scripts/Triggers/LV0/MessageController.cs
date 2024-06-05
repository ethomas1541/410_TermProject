// Hunter McMahon
// 5/10/2024
// M 5/10/2024
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MessageController : MonoBehaviour
{
    private string[] Messages = {
        "Left Click to swing Axe, Chop Down Trees To Harvest Wood",
        "Swing your axe To ward off the Lorax's Critters. (Careful they Bite)",
        "Head North to your camp, It is Key to your logging operation",
        "Use the camp Workbench to purchase upgrades for your camp. Paul can upgrade himself at the camp wagon",
        "Uh Oh! The Lorax has found you. DEFEND THE CAMP WITH YOUR LIFE!!!",
        " "};
    private int MSGIndex = 0;
    public TextMeshProUGUI Instruction;

    public CampUpgradeController UpgCtrl;
    public HealthController EnemyHC;

    private bool MenuUnopened = true;
    private bool firstfightdone = true;
    

    void Awake() {
        UpgCtrl.OnExitMenu += OnMenuClose;
        EnemyHC.OnDeath += OnFirstFightWin;
    }

    public void ChangeMessage()
    {
        Instruction.text = Messages[MSGIndex];
        MSGIndex ++;
    }

    public void OnMenuClose()
    {
        if (MenuUnopened)
        {
            MSGIndex = 4;
            Instruction.text = Messages[MSGIndex];
            MSGIndex ++;
            MenuUnopened = false;
        }   
    }

    public void OnFirstFightWin()
    {
        if(firstfightdone)
        {
            Instruction.text = Messages[MSGIndex];
            MSGIndex ++;
            firstfightdone = false;
        }
    }
}
