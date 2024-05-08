using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class WeaponController : MonoBehaviour
{
    public int damage;
    private Collider weaponCollider;
    private Transform defaultParent;

    void Start() {
        defaultParent = GetComponentInParent<Transform>();
        weaponCollider = GetComponent<Collider>();
        EndAttack();
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
        weaponCollider.enabled = true;
    }

    public void EndAttack() {
        weaponCollider.enabled = false;
    }
}
