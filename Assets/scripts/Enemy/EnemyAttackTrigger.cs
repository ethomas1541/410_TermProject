using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class EnemyAttackTrigger : MonoBehaviour
{
    public Enemy enemy;

    void Start()
    {
        enemy = GetComponentInParent<Enemy>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == enemy.target.tag)
        {
            enemy.isApproaching = false;
            enemy.isAttacking = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == enemy.target.tag)
        {
            enemy.isApproaching = true;
            enemy.isAttacking = false;
        }
    }
}
