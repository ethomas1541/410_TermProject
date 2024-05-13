public class EnemyHitState : EnemyState
{
    public EnemyHitState(Enemy enemy, EnemyStateMachine stateMachine) : base(enemy, stateMachine) {}

    public override void EnterState() {
        enemy.audioSource.clip = enemy.hitAudio;
        enemy.audioSource.Play();
        enemy.gfxAnimator.Play("hit");
    }

    public override void ExitState()
    {
        enemy.isHit = false;
    }

    public override void FrameUpdate()
    {
        if (enemy.isDead) { stateMachine.ChangeState(enemy.dieState); }
    }

    public override void PhysicsUpdate() {}

    public override void AnimationTriggerEvent(EnemyAnimationListener.Type type)
    {
        if (type == EnemyAnimationListener.Type.EndHit) { stateMachine.ChangeState(enemy.approachState); }
    }

}