// https://www.youtube.com/watch?v=UjkSFoLxesw
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(HealthController))]
public class EnemyController : MonoBehaviour
{
    public Transform target;
    public float speed = 5.0f;
    public float attackSpeed = 0.5f;
    public float attackRadius = 1.0f;

    NavMeshAgent agent;
    Animator animator;
    HealthController healthController;
    DamageTrigger damageTrigger;

    bool alreadyAttacked;


    public void Initialize(Transform t)
    {
        target = t;
    }

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        healthController = GetComponent<HealthController>();
        animator = GetComponentInChildren<Animator>();
        damageTrigger = GetComponentInChildren<DamageTrigger>();

        // Subscribe to the on death method
        healthController.OnDeath += OnDie;

        agent.speed = speed;
        agent.stoppingDistance = attackRadius;

        alreadyAttacked = false;
    }

    void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }

    void FixedUpdate()
    {
        float targetDist = Vector3.Distance(transform.position, target.position);

        // Should I approach?
        if (targetDist > attackRadius) {
            ApproachTarget();
        }

        // Should I attack?
        else {
            AttackTarget();
        }
    }

    public void OnDie() {
        StartCoroutine(Kill());
    }

    IEnumerator Kill() {
        animator.SetBool("Approaching", false);
        animator.SetTrigger("Die");

        // Wait for the current transition to end
        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime <= 1.0f );

        // Wait for the death animation to end
        yield return new WaitWhile(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime <= 1.0f);

        transform.gameObject.SetActive(false);
    }

    void FaceTarget() {
        Vector3 direction = (target.position - transform.position).normalized;
        direction.y = 0.0f;
        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = rotation;
    }

    void ApproachTarget()
    {
        animator.SetBool("Approaching", true);
        agent.SetDestination(target.position);
    }

    void AttackTarget()
    {
        animator.SetBool("Approaching", false);

        FaceTarget();

        if (!alreadyAttacked) {
             StartCoroutine(Attack());
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), attackSpeed);
        }
    }

    IEnumerator Attack() {
        damageTrigger.EnableDamage();
        animator.SetTrigger("Attack");

        // Wait for the current transition to end
        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime <= 1.0f );

        // Wait for the attack animation to end
        yield return new WaitWhile(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime <= 1.0f);
        damageTrigger.EnableDamage();
    }

    void ResetAttack() {
        alreadyAttacked = false;
    }
}
