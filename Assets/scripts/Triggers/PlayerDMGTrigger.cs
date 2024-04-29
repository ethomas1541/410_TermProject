using UnityEngine;

public class PlayerDMGTrigger : MonoBehaviour
{
    // note this style of damage trigger only works if a rigidbody is attached to the object
   void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealthController playerHP = other.GetComponent<PlayerHealthController>();
            playerHP.TakeDamage(2.5f);
        }
        else if (other.CompareTag("Base")) {
            LoggingCamp lc = other.GetComponent<LoggingCamp>();
            lc.TakeDamage(2.5f);
        }
    }
}
