using System.Collections;
using UnityEngine;

[RequireComponent(typeof(HealthController))]
public class TreeController : MonoBehaviour
{
    HealthController healthController;
    Animator animator;

    void Start() {
        healthController = GetComponent<HealthController>();
        animator = GetComponentInChildren<Animator>();
    }

    void FixedUpdate()
    {
        if (healthController.GetHP() <= 0.0f) {
            StartCoroutine(Die());
        }
    }

    IEnumerator Die()
    {
        animator.SetTrigger("Fall");

        // Wait for the current transition to end
        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime <= 1.0f );

        // Wait for the death animation to end
        yield return new WaitWhile(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime <= 1.0f);

        transform.gameObject.SetActive(false);
    }
}
