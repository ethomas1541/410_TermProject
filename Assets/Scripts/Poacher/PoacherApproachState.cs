public class PoacherApproachState : PoacherState
{
    public PoacherApproachState(Poacher poacher, PoacherStateMachine stateMachine) : base(poacher, stateMachine) {}

    // We will include idle as a default state, but for now it just transitions to approach
    public override void ExitState()
    {
        poacher.isApproaching = false;
    }

    public override void FrameUpdate()
    {
        if (poacher.isAttacking) { stateMachine.ChangeState(poacher.attackState); }
        poacher.navMeshAgent.SetDestination(poacher.target.position);
    }
}