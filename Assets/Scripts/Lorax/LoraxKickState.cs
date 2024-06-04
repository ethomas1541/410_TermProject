public class LoraxKickState : LoraxState {
    private float prevStopDistance;

    public LoraxKickState(Lorax lorax, LoraxStateMachine stateMachine) : base(lorax, stateMachine) {}

    public override void EnterState()
    {
        prevStopDistance = lorax.navMeshAgent.stoppingDistance;
        lorax.navMeshAgent.stoppingDistance = 1;
        lorax.navMeshAgent.speed = 8;
        lorax.audioSource.clip = lorax.kickApproachAudio;
        lorax.audioSource.Play();
    }

    public override void ExitState()
    {
        lorax.navMeshAgent.stoppingDistance = prevStopDistance;
        lorax.navMeshAgent.speed = lorax.speed;
        lorax.isKicking = false;
        lorax.isAttacking = false;
    }

    public override void FrameUpdate()
    {
        float sqaredDistance = (lorax.transform.position - lorax.target.position).magnitude;

        if (sqaredDistance <= 2) {
            lorax.audioSource.clip = lorax.kickAudio;
            lorax.audioSource.Play();
            lorax.FaceTarget();
            lorax.gfxAnimator.SetTrigger("kick");
            lorax.target.GetComponent<HealthController>().TakeDamage(lorax.kickDamage);
            lorax.isApproaching = true;
            lorax.ResetShoot();
            lorax.stateMachine.ChangeState(lorax.approachState);
        }

        lorax.navMeshAgent.SetDestination(lorax.target.position);
    }
}