using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoacherAttackTrigger : MonoBehaviour
{
    public bool isAttack = false;
    public Poacher poacher;
    private Collider agroCollider;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy") {
            if (isAttack) {
                poacher.target = other.transform;
                poacher.isAttacking = true;
            } else {
                poacher.target = other.transform;
                poacher.isApproaching = true;
            }
        }
    }
}
