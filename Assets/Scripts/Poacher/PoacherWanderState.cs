using UnityEngine;
using UnityEngine.AI;

public class PoacherWanderState : PoacherState
{
    private float nextWanderTime;
    private Vector3 position;
    private bool wandering;

    public PoacherWanderState(Poacher poacher, PoacherStateMachine stateMachine) : base(poacher, stateMachine) {}

    public override void EnterState()
    {
        poacher.navMeshAgent.speed = poacher.walk_speed;
        nextWanderTime = Time.time;
        wandering = true;
        poacher.navMeshAgent.stoppingDistance = 0;
    }

    public override void ExitState()
    {
        poacher.navMeshAgent.speed = poacher.run_speed;
        poacher.navMeshAgent.stoppingDistance = poacher.attackTriggerCollider.radius;
        poacher.isWandering = false;
    }

    public override void PhysicsUpdate()
    {
        if (poacher.isApproaching) { stateMachine.ChangeState(poacher.approachState); }
        if (poacher.isAttacking) { stateMachine.ChangeState(poacher.attackState); }

        if (IsClose() && !wandering)
        {
            StartCooldown();
            wandering = true;
        }

        if (nextWanderTime <= Time.time && wandering) {
            position = RandomNavSphere(poacher.transform.position, 10, -1);
            poacher.navMeshAgent.SetDestination(position);
            wandering = false;
        }
    }

    private void StartCooldown()
    {
        nextWanderTime = poacher.wanderCooldown + Time.time;
    }

    private bool IsClose()
    {
        return (poacher.transform.position - position).sqrMagnitude <= (0.1 * 0.1);;
    }

    private static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask) {
        Vector3 randDirection = Random.insideUnitSphere * dist;

        randDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition (randDirection, out navHit, dist, layermask);

        return navHit.position;
    }
}