using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDMGTrigger : MonoBehaviour
{
    // note this style of damage trigger only works if a rigidbody is attached to the object
   void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            HealthController EnemyHP = other.GetComponent<HealthController>();
            EnemyHP.TakeDamage(20);
        }
    }
}
