using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponInventory : MonoBehaviour
{
    public List<WeaponController> weapons;
    public Transform weaponAnchor;
    private WeaponController currentWeapon;

    void Start()
    {
        weapons[0].Equipt(weaponAnchor);
        currentWeapon = weapons[0];
    }

    public WeaponController GetCurrentWeapon() {
        return currentWeapon;
    }
}
