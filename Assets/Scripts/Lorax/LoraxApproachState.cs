public class LoraxApproachState : LoraxState {
    public LoraxApproachState(Lorax lorax, LoraxStateMachine stateMachine) : base(lorax, stateMachine) {}

    public override void EnterState() {}
    public override void ExitState()
    {
        lorax.isApproaching = false;
    }

    public override void PhysicsUpdate()
    {
        if (lorax.isDead) { stateMachine.ChangeState(lorax.dieState); }
        else if (lorax.isKicking) { stateMachine.ChangeState(lorax.kickState); }
        else if (lorax.isShooting) { stateMachine.ChangeState(lorax.shootState); }

        lorax.navMeshAgent.SetDestination(lorax.target.position);
    }
}