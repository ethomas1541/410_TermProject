using UnityEngine;

public class PoacherAttackState : PoacherState
{
    private float nextAttackTime;
    private bool isCoolingDown => Time.time <= nextAttackTime;

    public PoacherAttackState(Poacher poacher, PoacherStateMachine stateMachine) : base(poacher, stateMachine) {}

    public override void EnterState()
    {
        nextAttackTime = Time.time;
        poacher.audioSource.clip = poacher.attackAudio;
        // enemy.audioSource.clip = enemy.attackAudio;
    }

    public override void ExitState() {
        poacher.isAttacking = false;
    }

    public override void FrameUpdate()
    {
        // We need to get closer
        if (poacher.isApproaching) { stateMachine.ChangeState(poacher.approachState); }

        // Target is dead
        if (poacher.target == null) { stateMachine.ChangeState(poacher.wanderState); }

        if (!isCoolingDown)
        {
            poacher.gfxAnimator.SetTrigger("shoot");
            poacher.audioSource.Play();
            // enemy.audioSource.Play();
            HealthController targetHealth =  poacher.target.GetComponent<HealthController>();
            targetHealth.TakeDamage(poacher.attackDamage);
            if (targetHealth.currentHealth <= 0) { stateMachine.ChangeState(poacher.wanderState); }
            StartCooldown();
        }
    }

    public override void PhysicsUpdate()
    {
        FaceTarget();
    }

    private void FaceTarget()
    {
        Vector3 direction = (poacher.target.position - poacher.transform.position).normalized;
        direction.y = 0.0f;
        Quaternion rotation = Quaternion.LookRotation(direction);
        poacher.transform.rotation = rotation;
    }

    private void StartCooldown()
    {
        nextAttackTime = poacher.attackCooldown + Time.time;
    }
}