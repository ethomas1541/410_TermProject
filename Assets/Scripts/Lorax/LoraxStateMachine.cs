public class LoraxStateMachine {
    public LoraxState CurrentState { get; set; }

    public void Initialize(LoraxState state)
    {
        CurrentState = state;
        CurrentState.EnterState();

    }

    public void ChangeState(LoraxState state)
    {
        CurrentState.ExitState();
        CurrentState = state;
        CurrentState.EnterState();
    }
}