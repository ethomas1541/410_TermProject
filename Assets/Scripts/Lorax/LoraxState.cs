public class LoraxState {
    protected Lorax lorax;
    protected LoraxStateMachine stateMachine;

    public LoraxState(Lorax lorax, LoraxStateMachine stateMachine)
    {
        this.lorax = lorax;
        this.stateMachine = stateMachine;
    }

    public virtual void EnterState() {}
    public virtual void ExitState() {}
    public virtual void FrameUpdate() {}
    public virtual void PhysicsUpdate() {}
}