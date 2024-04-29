using UnityEngine;

public class PlayerDMGTrigger : MonoBehaviour
{
    // note this style of damage trigger only works if a rigidbody is attached to the object
   void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealthController playerHP = other.GetComponent<PlayerHealthController>();
            playerHP.TakeDamage(5);
        }
    }
}
