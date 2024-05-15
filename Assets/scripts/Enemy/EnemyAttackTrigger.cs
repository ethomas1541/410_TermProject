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
            // If the enemy is agro on the camp, but encounter a wall attack the wall instead
            if (other != enemy.target) { enemy.target = other.transform; }

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
