public class PoacherState {
    protected Poacher poacher;
    protected PoacherStateMachine stateMachine;

    public PoacherState(Poacher poacher, PoacherStateMachine stateMachine)
    {
        this.poacher = poacher;
        this.stateMachine = stateMachine;
    }

    public virtual void EnterState() {}
    public virtual void ExitState() {}
    public virtual void FrameUpdate() {}
    public virtual void PhysicsUpdate() {}
}