using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(NavMeshAgent))]
public class Poacher : MonoBehaviour
{
    [Header("Poacher Stats")]
    public float walk_speed = 5;
    public float run_speed= 9;
    public float wanderCooldown = 5;
    public int attackDamage = 25;
    public float attackCooldown = 0.5f;

    [Header("Poacher Audio")]
    public AudioClip approachAudio;
    public AudioClip attackAudio;

    [Header("Enemy State Booleans")]
    public bool isWandering;
    public bool isApproaching;
    public bool isAttacking;

    [Header("Components for State Machine")]
    public BoxCollider boxCollider;
    public NavMeshAgent navMeshAgent;
    public AudioSource audioSource;
    public Animator gfxAnimator;
    public SphereCollider attackTriggerCollider;
    public Transform target;

    #region state machine variables
    public PoacherStateMachine stateMachine;
    public PoacherWanderState wanderState;
    public PoacherApproachState approachState;
    public PoacherAttackState attackState;
    #endregion

    void Awake()
    {
        // Setup states
        stateMachine = new PoacherStateMachine();
        wanderState = new PoacherWanderState(this, stateMachine);
        approachState = new PoacherApproachState(this, stateMachine);
        attackState = new PoacherAttackState(this, stateMachine);
    }

    void Start()
    {
        // Get our components
        boxCollider = GetComponent<BoxCollider>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        audioSource = GetComponent<AudioSource>();
        gfxAnimator = GetComponentInChildren<Animator>();
        // attackTriggerCollider = GetComponentInChildren<SphereCollider>();
        // target = GameObject.FindWithTag(targetTag).transform;

        // Set the nav mesh agent attributes
        navMeshAgent.acceleration = 999;
        navMeshAgent.angularSpeed = 120;
        // navMeshAgent.autoRepath = true;

        // Set the starting state
        stateMachine.Initialize(wanderState);
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
}