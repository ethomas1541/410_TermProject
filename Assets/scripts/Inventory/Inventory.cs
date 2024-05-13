/* 
Code from: Simple Inventory System in Unity (Store, Use, Stack and Drop Items)
    https://www.youtube.com/watch?v=2WnAOV7nHW0
*/

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Inventory
{
    private List<Item> itemList;

    public Inventory()
    {
        itemList = new List<Item>();

        AddItem(new Item { itemType = Item.ItemType.Axe, amount = 1 });
        AddItem(new Item { itemType = Item.ItemType.Wood, amount = 10});

        //Debug.Log(itemList[1].itemType + " " + itemList[1].amount);
    }

    public void AddItem(Item item)
    {
        itemList.Add(item);
    }

    public List<Item> GetItemList()
    {
        return itemList;
    }


}