using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DamageTrigger))]
public class WeaponController : MonoBehaviour
{
    private Transform defaultParent;
    private DamageTrigger damageTrigger;

    void Start() {
        defaultParent = GetComponentInParent<Transform>();
        damageTrigger = GetComponent<DamageTrigger>();
        damageTrigger.DisableDamage();
    }

    public void Equipt(Transform anchor) {
        transform.gameObject.SetActive(true);
        transform.SetParent(anchor);
    }

    public void Unequipt() {
        transform.gameObject.SetActive(false);
        transform.SetParent(defaultParent);
    }

    public void StartAttack() {
        damageTrigger.EnableDamage();
    }

    public void EndAttack() {
        damageTrigger.DisableDamage();
    }
}
