using UnityEngine;
using UnityEngine.AI;

public class Lorax : MonoBehaviour
{
    [Header("Lorax Stats")]
    public int maxHealth = 150;
    public float speed = 5;
    public int kickDamage = 10;
    public int shootDamage = 5;
    public float shootCooldown = 0.5f;

    [Header("Lorax Audio")]
    public AudioClip[] shootAudio;
    public AudioClip[] kickAudio;
    public AudioClip deathAudio;

    [Header("Lorax State Booleans")]
    public bool isApproaching;
    public bool isAttacking;
    public bool isKicking;
    public bool isShooting;
    public bool isDead;

    #region state machine variables
    public LoraxStateMachine stateMachine;
    public LoraxApproachState approachState;
    public LoraxKickState kickState;
    public LoraxShootState shootState;
    public LoraxDieState dieState;
    #endregion

    // Attack combos
    private int timesToShootRNG = 4;
    public int timesToShoot = 3;
    public int timesShot = 0;

    [Header("Components for State Machine")]
    public CapsuleCollider capsuleCollider;
    public Rigidbody rigidBody;
    public NavMeshAgent navMeshAgent;
    public AudioSource audioSource;
    public HealthController healthController;
    public Animator gfxAnimator;
    public SphereCollider attackTriggerCollider;
    public Transform target;

    void Awake()
    {
        stateMachine = new LoraxStateMachine();
        approachState = new LoraxApproachState(this, stateMachine);
        kickState = new LoraxKickState(this, stateMachine);
        shootState = new LoraxShootState(this, stateMachine);
        dieState = new LoraxDieState(this, stateMachine);
    }

    void Start()
    {
        // Get our components
        capsuleCollider = GetComponent<CapsuleCollider>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        audioSource = GetComponent<AudioSource>();
        gfxAnimator = GetComponentInChildren<Animator>();
        healthController = GetComponent<HealthController>();
        target = GameObject.FindWithTag("Player").transform;

        // Set the nav mesh agent attributes
        navMeshAgent.speed = speed;
        navMeshAgent.acceleration = 999;
        navMeshAgent.angularSpeed = 120;

        // Set our health
        healthController.initialHealth = maxHealth;
        healthController.currentHealth = maxHealth;

        healthController.OnDeath += () => isDead = true;

        // Set the starting state
        stateMachine.Initialize(approachState);
    }

    void Update()
    {
        stateMachine.CurrentState.FrameUpdate();
    }

    void FixedUpdate()
    {
        stateMachine.CurrentState.PhysicsUpdate();
        gfxAnimator.SetFloat("speed", navMeshAgent.velocity.magnitude);
    }

    public void ResetShoot()
    {
        timesShot = 0;
        timesToShoot = Random.Range(1, timesToShootRNG);
    }

    public void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        direction.y = 0.0f;
        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = rotation;
    }
}