using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(HealthController))]
public class TreeController : MonoBehaviour
{
    HealthController healthController;
    Animator animator;

    // let the tree access the players inventory
    public int WoodValue = 10;
    //public GameObject Player;
    public WoodInventory Wallet;

    void Awake() {
        healthController = GetComponent<HealthController>();
        animator = GetComponentInChildren<Animator>();

        // Subscribe to the OnDeath event
        healthController.OnDeath += OnDie;
    }

    public void OnDie() {
        StartCoroutine(Kill());
    }

    IEnumerator Kill()
    {
        animator.SetTrigger("Fall");

        // Wait for the current transition to end
        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime <= 1.0f );

        // Wait for the death animation to end
        yield return new WaitWhile(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime <= 1.0f);
        Wallet.AddWood(WoodValue);
        transform.gameObject.SetActive(false);
    }
}
