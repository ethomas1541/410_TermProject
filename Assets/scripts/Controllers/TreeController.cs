// 5/15/24 - adding respawn coroutine - Hunter
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(HealthController))]
public class TreeController : MonoBehaviour
{
    public HealthController healthController;
    public Animator animator;

    // let the tree access the players inventory
    public int WoodValue = 10;
    //public GameObject Player;
    public WoodInventory Wallet;
    //stump to replace with when choped down
    public GameObject Stump;
    public GameObject TreeModel;
    public Transform TreeGFX;

    void Awake() {
        // Subscribe to the OnDeath event
        healthController.OnDeath += OnDie;
        // store default positions of gfx object
    }

    public void OnDie() {
        StartCoroutine(Kill());
        StartCoroutine(Respawn());
        // this is so the player cant wack the stump for infinite wood
        healthController.Heal(5000);
    }

    IEnumerator Kill()
    {
        animator.SetTrigger("Fall");

        // Wait for the current transition to end
        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime <= 1.0f );
        
        Stump.SetActive(true);
        Wallet.AddWood(WoodValue);

        // Wait for the death animation to end
        yield return new WaitWhile(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime <= 1.0f);
        TreeModel.SetActive(false);
    }

    IEnumerator Respawn()
    {   
        //80 is final value for now
        //yield return new WaitForSeconds(80f);
        yield return new WaitForSeconds(5f);
        Stump.SetActive(false);
        // now set the health back to normal
        healthController.ResetHealth();
        TreeModel.SetActive(true);
        
        TreeGFX.position = new Vector3(0f, 0f, 0f); 
        TreeGFX.rotation = Quaternion.Euler(0f, 0f, 0f); 

    }
}
