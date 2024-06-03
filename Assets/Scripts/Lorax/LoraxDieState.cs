public class LoraxDieState : LoraxState {
    public LoraxDieState(Lorax lorax, LoraxStateMachine stateMachine) : base(lorax, stateMachine) {}

    public override void EnterState()
    {
        lorax.gfxAnimator.SetTrigger("die");
    }

    public override void ExitState()
    {

    }

    public override void FrameUpdate()
    {

    }

}