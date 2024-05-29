using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterController : MonoBehaviour
{
    // The amount of force to apply to the player
    public float riverForce = 10f;
    public Vector3 forceDirection;

    private void Start()
    {
        forceDirection = transform.right;
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" || other.tag == "Enemy" || other.tag == "Poacher") {
            // Apply force to the player in the calculated direction
            Rigidbody playerRigidbody = other.GetComponent<Rigidbody>();
            playerRigidbody.AddForce(forceDirection * riverForce, ForceMode.Force);
        }
    }
}
