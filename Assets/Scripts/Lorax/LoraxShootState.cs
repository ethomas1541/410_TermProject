using UnityEngine;

public class LoraxShootState : LoraxState {
    private float nextAttackTime;
    private bool isCoolingDown => Time.time <= nextAttackTime;

    public LoraxShootState(Lorax lorax, LoraxStateMachine stateMachine) : base(lorax, stateMachine) {}

    public override void EnterState()
    {
        nextAttackTime = Time.time;
    }

    public override void ExitState()
    {
        lorax.isShooting = false;
        lorax.isAttacking = false;
    }

    public override void FrameUpdate()
    {
        if (lorax.isDead) { stateMachine.ChangeState(lorax.dieState); }
        else if (lorax.isKicking) { stateMachine.ChangeState(lorax.kickState); }
        else if (lorax.isApproaching) { stateMachine.ChangeState(lorax.approachState); }

        lorax.FaceTarget();
        lorax.isKicking = lorax.timesShot >= lorax.timesToShoot;

        if (!isCoolingDown && !lorax.isKicking) {
            lorax.gfxAnimator.SetTrigger("shoot");
            lorax.target.GetComponent<HealthController>().TakeDamage(lorax.shootDamage);
            lorax.timesShot++;
            StartCooldown();
        }
    }

    private void StartCooldown()
    {
        nextAttackTime = lorax.shootCooldown + Time.time;
    }
}