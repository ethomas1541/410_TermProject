/* 
Code from: Simple Inventory System in Unity (Store, Use, Stack and Drop Items)
    https://www.youtube.com/watch?v=2WnAOV7nHW0
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Inventory : MonoBehaviour
{
    private Inventory inventory;
    private Transform ItemSlotContainer;
    private Transform ItemSlotContainerTemplate;

    private void Start()
    {
        ItemSlotContainer = transform.Find("ItemSlotContainer");
        ItemSlotContainerTemplate = ItemSlotContainer.Find("ItemSlotContainerTemplate");
    }

    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;
        RefreshInventoryItems();
    }

    /// Refreshes the inventory items and updates the UI.
    private void RefreshInventoryItems()
    {
        int x = 0;
        int y = 0;

        float itemSlotCellSize = 120f;

        foreach (Item item in inventory.GetItemList())
        {
            // Debug.Log(ItemSlotContainerTemplate);
            // Debug.Log(ItemSlotContainer);
            RectTransform itemSlotRectTransform = Instantiate(ItemSlotContainerTemplate, ItemSlotContainer).GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);
            itemSlotRectTransform.anchoredPosition = new Vector2(x * itemSlotCellSize, y * itemSlotCellSize);

           // Debug.Log(itemSlotRectTransform.Find("Image"));
            Image image = itemSlotRectTransform.Find("Image").GetComponent<Image>();
            image.sprite = item.GetSprite();

            x++;

            if (x > 4)
            {
                x = 0;
                y++;
            }
        }
    }
}
