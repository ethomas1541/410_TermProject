using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MessageController : MonoBehaviour
{
    private string[] Messages = {
        "Left Click to swing Axe, Chop Down Trees To Harvest Wood",
        "Swing your axe To ward off the Lorax's Critters (Careful they Bite)",
        "Head North to your camp, It is Key to your logging operation",
        "Upgrade your camp with wood at the workbench",
        "The Lorax's critters will try to stop you, defend yourself and your camp!",
        " "};
    private int MSGIndex = 0;
    public TextMeshProUGUI Instruction;
    public void ChangeMessage()
    {
        Instruction.text = Messages[MSGIndex];
        MSGIndex ++;
    }
}
