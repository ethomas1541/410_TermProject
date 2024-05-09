using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class OpenUpgradeMenu : MonoBehaviour
{
    public GameObject upgradeMenu;
    private bool isPlayerNearby = false;
    public KeyCode openMenuKey = KeyCode.F;

    public void Start()
    {
        upgradeMenu.SetActive(false);
    }

// couldnt get this to work with the new unity input system 
    void Update()
    {
        // Check if the player is nearby and presses the designated key to open the menu
        if (isPlayerNearby && Input.GetKeyDown(openMenuKey))
        {
            // Open the upgrade menu when the player is nearby and presses the key
            upgradeMenu.SetActive(true);
        }
    } 

    private void OnTriggerEnter(Collider other)
    {
        // Check if the entering collider is tagged as "Player"
        if (other.CompareTag("Player"))
        {
            // Check if the player is close enough to the workbench
            isPlayerNearby = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the exiting collider is tagged as "Player"
        if (other.CompareTag("Player"))
        {
            // Player is no longer nearby
            isPlayerNearby = false;
        }
    }
}

