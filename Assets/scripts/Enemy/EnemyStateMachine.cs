using UnityEngine;

public class EnemyStateMachine {
    public EnemyState CurrentState { get; set; }

    public void Initialize(EnemyState state)
    {
        CurrentState = state;
        CurrentState.EnterState();

    }

    public void ChangeState(EnemyState state)
    {
        CurrentState.ExitState();
        CurrentState = state;
        CurrentState.EnterState();
    }
}