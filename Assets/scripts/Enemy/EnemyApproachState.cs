public class EnemyApproachState : EnemyState
{
    public EnemyApproachState(Enemy enemy, EnemyStateMachine stateMachine) : base(enemy, stateMachine)
    {
    }

    public override void EnterState()
    {
        enemy.gfxAnimator.Play("approach");
    }

    public override void ExitState()
    {
        enemy.isApproaching = false;
    }

    public override void FrameUpdate()
    {
        // death takes priority over all statess
        if (enemy.isDead) { stateMachine.ChangeState(enemy.dieState); }
        else if (enemy.isHit) { stateMachine.ChangeState(enemy.hitState); }
        else if (enemy.isAttacking) { stateMachine.ChangeState(enemy.attackState); }
    }

    public override void PhysicsUpdate()
    {
        enemy.navMeshAgent.SetDestination(enemy.target.position);
    }

    public override void AnimationTriggerEvent(EnemyAnimationListener.Type type) {}
}