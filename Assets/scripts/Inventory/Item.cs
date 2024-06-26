/* 
Code from: Simple Inventory System in Unity (Store, Use, Stack and Drop Items)
    https://www.youtube.com/watch?v=2WnAOV7nHW0
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public enum ItemType
    {
        Axe,
        Wood,
    }

    public ItemType itemType;
    public int amount;

    public Sprite GetSprite()
    {
        switch (itemType)
        {
            default:
            case ItemType.Axe: return ItemAssets.Instance.AxeSprite;
            case ItemType.Wood: return ItemAssets.Instance.WoodSprite;
        }
    }
}