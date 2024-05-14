using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ToggleResourceInventory : MonoBehaviour
{
    public GameObject UI_Inventory;
    public KeyCode toggleUIKey = KeyCode.I;

    public void Start()
    {
        
    }

    void Update()
    {
        // Check if the player presses the toggle resource inventory key
        if (Input.GetKeyDown(toggleUIKey) && !UI_Inventory.activeSelf)
        {
            UI_Inventory.SetActive(true);
        }
        else if (Input.GetKeyDown(toggleUIKey) && UI_Inventory.activeSelf)
        {
            UI_Inventory.SetActive(false);
        }
    } 
}

