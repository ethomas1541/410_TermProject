using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TagDamage {
    public string tag;
    public int damage;
}

[RequireComponent(typeof(Collider))]
public class DamageTrigger : MonoBehaviour
{
    public List<TagDamage> tagDamages = new List<TagDamage>();
    private Collider damageCollider;

    void Start() {
        damageCollider = GetComponent<Collider>();
        DisableDamage();
    }

    public void EnableDamage() {
        damageCollider.enabled = true;
    }

    public void DisableDamage() {
        damageCollider.enabled = false;
    }

    // note this style of damage trigger only works if a rigidbody is attached to the object
   void OnTriggerEnter(Collider other)
    {
        foreach (TagDamage td in tagDamages) {
            if (other.tag == td.tag) {
                other.GetComponent<HealthController>().TakeDamage(td.damage);
            }
        }
    }
}
