public class LoraxDieState : LoraxState {
    public LoraxDieState(Lorax lorax, LoraxStateMachine stateMachine) : base(lorax, stateMachine) {}

    public override void EnterState()
    {
        lorax.gfxAnimator.SetTrigger("die");
        lorax.audioSource.clip = lorax.deathAudio;
        lorax.audioSource.Play();
    }

    public override void ExitState()
    {

    }

    public override void FrameUpdate()
    {

    }

}