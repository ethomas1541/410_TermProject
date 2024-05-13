using UnityEngine;

public class EnemyAnimationListener : MonoBehaviour
{
    private Enemy enemy;

    void Awake()
    {
        enemy = GetComponentInParent<Enemy>();
    }

    public void AnimationTriggerEvent(Type type)
    {
        enemy.AnimationTriggerEvent(type);
    }

    public enum Type
    {
        BeginAttack,
        EndAttack,
        EndHit,
        EndDie,
    }
}
