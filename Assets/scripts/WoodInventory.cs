using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodInventory : MonoBehaviour
{   
    // starting wood value, can be adjusted latter
    public int WoodAmount = 10;

    public void AddWood(int x)
    {
        WoodAmount += x;
    }

    public void SpendWood(int x)
    {
        WoodAmount -= x;
        // safegaurd from going into dept
        if (WoodAmount < 0)
        {
            WoodAmount = 0;
        }
    }
}
