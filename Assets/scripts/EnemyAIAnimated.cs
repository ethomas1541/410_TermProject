// https://www.youtube.com/watch?v=UjkSFoLxesw
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAIAnimated : MonoBehaviour
{
    public GameObject primaryTarget, secondaryTarget;
    public LayerMask ground, player, camp;
    public float attackRange;

    NavMeshAgent agent;
    Animator animator;
    bool isPrimaryInAttackRange, isSecondaryInAttackRange;
    Quaternion angleCorrection;
    int primaryTargetLayerMask, secondaryTargetLayerMask;
    bool is_dead, is_killed;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        agent.stoppingDistance = attackRange;
        angleCorrection = Quaternion.AngleAxis(-90, Vector3.up);

        primaryTargetLayerMask = 1 << primaryTarget.layer;
        secondaryTargetLayerMask = 1 << secondaryTarget.layer;
    }

    private void FixedUpdate()
    {

        if (Input.GetKey(KeyCode.K)) { is_dead = true; }

        if (is_dead && (!is_killed))
        {
            is_killed = true;
            Die();
        }
        else if (!is_dead)
        {
            isPrimaryInAttackRange = Physics.CheckSphere(transform.position, attackRange, primaryTargetLayerMask);
            isSecondaryInAttackRange = Physics.CheckSphere(transform.position, attackRange, secondaryTargetLayerMask);

            // The primary and secondary target can be attacked
            if (isPrimaryInAttackRange && isSecondaryInAttackRange)
            {
                float distPrimary = Vector3.SqrMagnitude(transform.position - primaryTarget.transform.position);
                float distSecondary = Vector3.SqrMagnitude(transform.position - secondaryTarget.transform.position);

                // Attack the nearest target
                if (distPrimary > distSecondary)
                {
                    AttackTarget(primaryTarget);
                }
                else
                {
                    AttackTarget(secondaryTarget);
                }
            }

            // The primary target can be attacked
            else if (isPrimaryInAttackRange)
            {
                AttackTarget(primaryTarget);
            }

            // The secondary target can be attacked
            else if (isSecondaryInAttackRange)
            {
                AttackTarget(secondaryTarget);
            }

            // No targets are in range, approach the primary target
            else
            {
                ApproachTarget();
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    private void PointTowards(GameObject target)
    {
        Vector3 direction = target.transform.position - transform.position;
        direction.y = 0;
        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = angleCorrection * rotation;
    }

    public void Die()
    {
        animator.SetBool("Approach", false);
        animator.SetBool("Attack", false);
        animator.SetTrigger("Dead");
        // this.gameObject.SetActive(false);
    }

    private void ApproachTarget()
    {
        PointTowards(primaryTarget);
        agent.SetDestination(primaryTarget.transform.position);
        animator.SetBool("Attack", false);
        animator.SetBool("Approach", true);
    }

    private void AttackTarget(GameObject target)
    {
        // Add attack code
        PointTowards(primaryTarget);
        animator.SetBool("Approach", false);
        animator.SetBool("Attack", true);
    }

}
