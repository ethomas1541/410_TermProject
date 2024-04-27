// https://www.youtube.com/watch?v=UjkSFoLxesw
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public Transform target;
    public float speed = 5.0f;
    public float attackSpeed = 0.5f;
    public float attackRadius = 1.0f;

    NavMeshAgent agent;
    Animator animator;

    bool alreadyAttacked;
    bool isDead;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();

        agent.speed = speed;
        agent.stoppingDistance = attackRadius;

        alreadyAttacked = false;
        isDead = false;
    }

    void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }

    void FixedUpdate()
    {
        float targetDist = Vector3.Distance(transform.position, target.position);
        if (isDead) {
            StartCoroutine(Kill());
        }
        else if (targetDist > attackRadius) {
            ApproachTarget();
        }
        else {
            AttackTarget();
        }
    }

    public void Die()
    {
        isDead = true;
    }

    public IEnumerator Kill() {
        Debug.Log("I DIED");
        animator.SetBool("Approaching", false);
        animator.SetTrigger("Die");
        yield return new WaitForSeconds(0.5f);
        transform.gameObject.SetActive(false);
    }

    void ApproachTarget()
    {
        animator.SetBool("Approaching", true);
        agent.SetDestination(target.position);
    }

    void AttackTarget()
    {
        animator.SetBool("Approaching", false);

        if (!alreadyAttacked) {
            animator.SetTrigger("Attack");
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), attackSpeed);
        }
    }

    void ResetAttack() {
        alreadyAttacked = false;
    }

}
