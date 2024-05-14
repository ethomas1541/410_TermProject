using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyDieState : EnemyState
{
    public EnemyDieState(Enemy enemy, EnemyStateMachine stateMachine) : base(enemy, stateMachine)
    {
    }

    public override void EnterState()
    {
        enemy.audioSource.clip = enemy.deathAudio;
        enemy.audioSource.Play();
        enemy.gfxAnimator.Play("dead");
    }

    public override void ExitState()
    {
        enemy.isDead = false;
    }

    public override void FrameUpdate() {}
    public override void PhysicsUpdate() {}

    public override void AnimationTriggerEvent(EnemyAnimationListener.Type type)
    {
        if (type == EnemyAnimationListener.Type.EndDie) {

            if (enemy.waveSpawner != null) { enemy.waveSpawner.Enemykilled(); }
            Object.Destroy(enemy.gameObject, enemy.deathAudio.length);
        }
    }
}