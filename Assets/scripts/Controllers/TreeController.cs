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

    // public GameObject Player;
    public WoodInventory Wallet;

    // stump to replace with when choped down
    public GameObject Stump;
    public GameObject TreeModel;

    private BoxCollider treeCollider;

    void Awake() {
        treeCollider = GetComponent<BoxCollider>();

        // Subscribe to the OnDeath event
        healthController.OnDeath += OnDie;
    }

    public void OnDie() {
        StartCoroutine(Kill());
        StartCoroutine(Respawn());

        // this is so the player cant wack the stump for infinite wood
        treeCollider.enabled = false;
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
        // This will play before it is active
        // this was giving a warning and seemed to not effect execution, commenting out and revisiting later - hunter
        //animator.SetTrigger("Stand");

        
        yield return new WaitForSeconds(80f);
        // 4 testing reasons
        //yield return new WaitForSeconds(5f);

        Stump.SetActive(false);

        // now set the health back to normal
        healthController.ResetHealth();
        TreeModel.SetActive(true);
        treeCollider.enabled = true;

    }
}
