// https://www.youtube.com/watch?v=UjkSFoLxesw
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public GameObject primaryTarget, secondaryTarget;
    public LayerMask ground, player, camp;
    public float attackRange;

    NavMeshAgent agent;
    bool isPrimaryInAttackRange, isSecondaryInAttackRange;

    private void Awake() {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update() {

        isPrimaryInAttackRange = Physics.CheckSphere(transform.position, attackRange, primaryTarget.layer);
        isSecondaryInAttackRange = Physics.CheckSphere(transform.position, attackRange, secondaryTarget.layer);

        // The primary and secondary target can be attacked
        if (isPrimaryInAttackRange && isSecondaryInAttackRange) {
            float distPrimary = Vector3.SqrMagnitude(transform.position - primaryTarget.transform.position);
            float distSecondary = Vector3.SqrMagnitude(transform.position - secondaryTarget.transform.position);

            // Attack the nearest target
            if (distPrimary > distSecondary) {
                AttackTarget(primaryTarget);
            } else {
                AttackTarget(secondaryTarget);
            }
        }

        // The primary target can be attacked
        else if (isPrimaryInAttackRange) {
            AttackTarget(primaryTarget);
        }

        // The secondary target can be attacked
        else if (isSecondaryInAttackRange){
            AttackTarget(secondaryTarget);
        }

        // No targets are in range, approach the primary target
        else {
            ApproachTarget();
        }
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    private void ApproachTarget() {
        agent.SetDestination(primaryTarget.transform.position);
    }

    private void AttackTarget(GameObject target) {
        // Add attack code

        // Stop enemy from moving during an attack
        agent.SetDestination(transform.position);
        transform.LookAt(target.transform);
    }
}
