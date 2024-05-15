// 5/15/24 - adding respawn coroutine - Hunter
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(HealthController))]
public class TreePhysicsController : MonoBehaviour
{
    public HealthController healthController;
    //public Animator animator;

    // let the tree access the players inventory
    public int WoodValue = 10;

    // public GameObject Player;
    public WoodInventory Wallet;

    // stump to replace with when choped down
    public GameObject Stump;
    public GameObject TreeModel;

    private BoxCollider treeCollider;

    private List<Transform> pieces;

    void Awake() {
        pieces = new List<Transform>();

        treeCollider = GetComponent<BoxCollider>();

        // Subscribe to the OnDeath event
        healthController.OnDeath += OnDie;

        foreach(Transform child in TreeModel.transform.GetChild(0).transform){
            pieces.Add(child);
            var rb = child.gameObject.GetComponent<Rigidbody>();
            rb.detectCollisions = false;
            rb.useGravity = false;
        }
    }

    public void OnDie() {
        Kill();
        StartCoroutine(Respawn());

        // this is so the player cant wack the stump for infinite wood
        treeCollider.enabled = false;
    }

    void Kill()
    {
        //animator.SetTrigger("Fall");

        // Wait for the current transition to end
        //yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime <= 1.0f );

        foreach(Transform piece in pieces){
            var rb = piece.gameObject.GetComponent<Rigidbody>();
            rb.detectCollisions=true;
            rb.useGravity=true;
        }
        Stump.SetActive(true);
        Wallet.AddWood(WoodValue);

        // Wait for the death animation to end
        //yield return new WaitWhile(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime <= 1.0f);
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
