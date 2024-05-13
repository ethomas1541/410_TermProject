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
    public AudioClip attackClip, takeDamageClip, dieClip;

    NavMeshAgent agent;
    Animator animator;
    AudioSource audioSource;
    HealthController healthController;
    DamageTrigger damageTrigger;

    bool alreadyAttacked;

    // added to track number of enemies killed in mock wave - hunter
    // probably a beter way to do this; rework after alpha
    public MockWave WaveCtrl;


    public void Initialize(Transform t)
    {
        target = t;
    }

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        healthController = GetComponent<HealthController>();
        animator = GetComponentInChildren<Animator>();
        audioSource = GetComponent<AudioSource>();
        damageTrigger = GetComponentInChildren<DamageTrigger>();

        // Subscribe to the on death method
        healthController.OnDeath += OnDie;

        // Subscribe to the on take damage method
        healthController.OnTakeDamage += TakeDamage;

        agent.speed = speed;

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

        audioSource.clip = dieClip;
        audioSource.Play();

        // probably rework this eventually, this is for the alphademp:
        WaveCtrl.Enemykilled();

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

    public void TakeDamage() {
        audioSource.clip = takeDamageClip;
        audioSource.Play();
    }

    void ApproachTarget()
    {
        animator.SetBool("Approaching", true);
        agent.SetDestination(target.position);
    }

    void AttackTarget()
    {
        audioSource.clip = attackClip;
        audioSource.Play();

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
