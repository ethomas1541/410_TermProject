using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TagDamage {
    public string tag;
    public int damage;
}

public class DamageTrigger : MonoBehaviour
{
    public List<TagDamage> tagDamages = new List<TagDamage>();

    // note this style of damage trigger only works if a rigidbody is attached to the object
   void OnTriggerEnter(Collider other)
    {
        foreach (TagDamage td in tagDamages) {
            if (other.tag == td.tag) {
                Debug.Log($"Hit {other}");
                other.GetComponent<HealthController>().TakeDamage(td.damage);
            }
        }
    }
}
