using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationDamageController : MonoBehaviour
{

    WeaponInventory weaponInventory;

    void Start() {
        weaponInventory = GetComponentInParent<WeaponInventory>();
    }

    public void EnableDamage() {
        weaponInventory.GetCurrentWeapon().StartAttack();
    }

    public void DisableDamage() {
        weaponInventory.GetCurrentWeapon().EndAttack();
    }
}
