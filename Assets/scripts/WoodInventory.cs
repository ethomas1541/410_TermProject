using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WoodInventory : MonoBehaviour
{   
    // starting wood value, can be adjusted latter
    public int WoodAmount = 10;
    public TextMeshProUGUI WoodHUD;

    public void Start()
    {
        UpdateHUD();
    }
    public void AddWood(int x)
    {
        WoodAmount += x;
        UpdateHUD();
    }

    public void SpendWood(int x)
    {
        WoodAmount -= x;
        // safegaurd from going into dept
        if (WoodAmount < 0)
        {
            WoodAmount = 0;
        }
        UpdateHUD();
    }

    public void UpdateHUD()
    {
        WoodHUD.text = "Wood: " + WoodAmount;
    }
}
