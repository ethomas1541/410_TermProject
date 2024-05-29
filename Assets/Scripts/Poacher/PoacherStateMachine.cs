using UnityEngine;

public class PoacherStateMachine {
    public PoacherState CurrentState { get; set; }

    public void Initialize(PoacherState state)
    {
        CurrentState = state;
        CurrentState.EnterState();

    }

    public void ChangeState(PoacherState state)
    {
        CurrentState.ExitState();
        CurrentState = state;
        CurrentState.EnterState();
    }
}