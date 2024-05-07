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
        Debug.Log("Collider is active");
        weaponCollider.enabled = true;
    }

    public void EndAttack() {
        Debug.Log("Collider is inactive");
        weaponCollider.enabled = false;
    }
}
