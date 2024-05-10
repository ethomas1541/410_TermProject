// Hunter McMahon
// Basically a copy of Tree Controller
// 5/9/2024
// updated: 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallpiececontroller : MonoBehaviour
{
    HealthController healthController;
    //Animator animator;
    public CampUpgradeController UpgradeCTRL;
    

    void Awake() {
        healthController = GetComponent<HealthController>();
        //animator = GetComponentInChildren<Animator>();

        // Subscribe to the OnDeath event
        healthController.OnDeath += OnDie;
    }

    public void OnDie() {
        //StartCoroutine(Kill());
        transform.gameObject.SetActive(false);
        UpgradeCTRL.WallsCost += 10;
        healthController.ResetHealth();
    }

    //TODO: uncomment this when we get around to animating the walls:
    // IEnumerator Kill()
    //{
        //animator.SetTrigger("Fall");

        // Wait for the current transition to end
        //yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime <= 1.0f );

        // Wait for the death animation to end
        //yield return new WaitWhile(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime <= 1.0f);
        //Wallet.AddWood(WoodValue);
       // transform.gameObject.SetActive(false);
    //}
}
