using UnityEngine;

public class EnemyAttackState : EnemyState
{
    private float nextAttackTime;
    private bool isCoolingDown => Time.time <= nextAttackTime;

    public EnemyAttackState(Enemy enemy, EnemyStateMachine stateMachine) : base(enemy, stateMachine) {}

    public override void EnterState()
    {
        nextAttackTime = Time.time;
        enemy.audioSource.clip = enemy.attackAudio;
    }

    public override void ExitState() {
        enemy.isAttacking = false;
    }

    public override void FrameUpdate()
    {
        // death takes priority over all statess
        if (enemy.isDead) { stateMachine.ChangeState(enemy.dieState); }
        else if (enemy.isHit) { stateMachine.ChangeState(enemy.hitState); }
        else if (enemy.isApproaching) { stateMachine.ChangeState(enemy.approachState); }

        if (!isCoolingDown)
        {
            enemy.gfxAnimator.Play("attack", 0, 0);
            enemy.audioSource.Play();
            enemy.target.GetComponent<HealthController>().TakeDamage(enemy.attackDamage);
            StartCooldown();
        }
    }

    public override void PhysicsUpdate()
    {
        FaceTarget();
    }

    public override void AnimationTriggerEvent(EnemyAnimationListener.Type type) {}

    private void FaceTarget()
    {
        Vector3 direction = (enemy.target.position - enemy.transform.position).normalized;
        direction.y = 0.0f;
        Quaternion rotation = Quaternion.LookRotation(direction);
        enemy.transform.rotation = rotation;
    }

    private void StartCooldown()
    {
        nextAttackTime = enemy.attackCooldown + Time.time;
    }
}