using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(NavMeshObstacle))]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(HealthController))]
public class Enemy : MonoBehaviour
{
    public enum State
    {
        IsIdle,
        IsApproaching,
        IsHit,
        IsDead
    }

    [Header("Enemy Stats")]
    public int maxHealth;
    public float speed;
    public int attackDamage;
    public float attackCooldown;
    public string targetTag;

    [Header("Enemy Audio")]
    public AudioClip approachAudio;
    public AudioClip hitAudio;
    public AudioClip attackAudio;
    public AudioClip deathAudio;

    [Header("Enemy State Booleans")]
    public bool isApproaching;
    public bool isAttacking;
    public bool isHit;
    public bool isDead;

    [Header("Components for State Machine")]
    public BoxCollider boxCollider;
    public Rigidbody rigidBody;
    public NavMeshAgent navMeshAgent;
    public NavMeshObstacle navMeshObstacle;
    public AudioSource audioSource;
    public HealthController healthController;
    public Animator gfxAnimator;
    public SphereCollider attackTriggerCollider;
    public Transform target;
    public MockWave waveSpawner;

    #region state machine variables
    public EnemyStateMachine stateMachine;
    public EnemyIdleState idleState;
    public EnemyApproachState approachState;
    public EnemyAttackState attackState;
    public EnemyHitState hitState;
    public EnemyDieState dieState;
    #endregion

    void Awake()
    {
        // Setup states
        stateMachine = new EnemyStateMachine();
        idleState = new EnemyIdleState(this, stateMachine);
        approachState = new EnemyApproachState(this, stateMachine);
        attackState = new EnemyAttackState(this, stateMachine);
        hitState = new EnemyHitState(this, stateMachine);
        dieState = new EnemyDieState(this, stateMachine);
    }

    void Start()
    {
        // Get our components
        boxCollider = GetComponent<BoxCollider>();
        rigidBody = GetComponent<Rigidbody>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshObstacle = GetComponent<NavMeshObstacle>();
        audioSource = GetComponent<AudioSource>();
        healthController = GetComponent<HealthController>();
        gfxAnimator = GetComponentInChildren<Animator>();
        attackTriggerCollider = GetComponentInChildren<SphereCollider>();
        target = GameObject.FindWithTag(targetTag).transform;

        // Set the tag to enemy
        gameObject.tag = "Enemy";

        // This will stop rigidbody from randomly moving and rotating the enemy
        rigidBody.constraints = RigidbodyConstraints.FreezeAll;

        // Set the nav mesh agent attributes
        navMeshAgent.acceleration = 999;
        navMeshAgent.angularSpeed = 120;
        navMeshAgent.speed = speed;
        navMeshAgent.stoppingDistance = attackTriggerCollider.radius;
        navMeshAgent.autoRepath = true;

        // Set the nav mesh obstacle attributes
        navMeshObstacle.enabled = false;
        navMeshObstacle.center = boxCollider.center;
        navMeshObstacle.size = boxCollider.size;

        // Set our max health in the health controller
        healthController.initialHealth = maxHealth;
        healthController.currentHealth = maxHealth;

        // Subscribe to take damage and die events in the health controller
        healthController.OnTakeDamage += () => isHit = true;
        healthController.OnDeath += () => isDead = true;

        // Set the starting state
        stateMachine.Initialize(idleState);
    }

    void Update()
    {
        stateMachine.CurrentState.FrameUpdate();

        // Wall has been broken, find a new camp target
        if (!target.gameObject.activeInHierarchy) {
            FindNewTarget();
            stateMachine.ChangeState(idleState);
        }
    }

    void FixedUpdate()
    {
        stateMachine.CurrentState.PhysicsUpdate();

        if (healthController.currentHealth <= 0) {
            stateMachine.ChangeState(dieState);
        }
    }

    public void SetTarget(string targetTag)
    {
        this.targetTag = targetTag;
        target = GameObject.FindWithTag(targetTag).transform;
    }

    private void FindNewTarget() {
        target = GameObject.FindWithTag(targetTag).transform;
    }

    public void SetWaveSpawner(MockWave waveSpawner)
    {
        this.waveSpawner = waveSpawner;
    }

    public void AnimationTriggerEvent(EnemyAnimationListener.Type type)
    {
        stateMachine.CurrentState.AnimationTriggerEvent(type);
    }
}