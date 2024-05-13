public class EnemyIdleState : EnemyState
{
    public EnemyIdleState(Enemy enemy, EnemyStateMachine stateMachine) : base(enemy, stateMachine) {}

    // We will include idle as a default state, but for now it just transitions to approach
    public override void EnterState()
    {
        enemy.isApproaching = true;
        stateMachine.ChangeState(enemy.approachState);
    }

    public override void FrameUpdate()
    {
        // death takes priority over all statess
        if (enemy.isDead) { stateMachine.ChangeState(enemy.dieState); }
    }
}