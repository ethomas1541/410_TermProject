public class EnemyApproachState : EnemyState
{
    public EnemyApproachState(Enemy enemy, EnemyStateMachine stateMachine) : base(enemy, stateMachine)
    {
    }

    public override void EnterState()
    {
        enemy.navMeshObstacle.enabled = false;
        enemy.navMeshAgent.enabled = true;

        enemy.gfxAnimator.Play("approach");
    }

    public override void ExitState()
    {
        // Do this disble both then enable one to prevent warnings
        enemy.navMeshAgent.enabled = false;
        enemy.navMeshObstacle.enabled = true;
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