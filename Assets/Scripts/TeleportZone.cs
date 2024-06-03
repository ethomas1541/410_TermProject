using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportZone : MonoBehaviour
{
    public int DMGPunishment = 5;
    public Vector3 ResetPos;

    void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<HealthController>() != null)
        {
            other.GetComponent<HealthController>().TakeDamage(DMGPunishment);
        }

        other.transform.position = ResetPos;
    }
}
