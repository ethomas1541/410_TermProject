using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnTouch : MonoBehaviour
{
    private BoxCollider box;

    private GameObject paul;
    private HealthController paulHealth;
    private bool canDamage = false;
    private int damageCooldown = 150;
    // Start is called before the first frame update
    void Start()
    {
        box = GetComponent<BoxCollider>();
        paul = GameObject.Find("Paul");
        paulHealth = paul.GetComponent<HealthController>();
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider other){
        Debug.Log("Ouch!");
        if (other.gameObject == paul & canDamage){
            paulHealth.TakeDamage(5);
            canDamage = false;
        }
    }

    void FixedUpdate(){
        if(!canDamage){
            damageCooldown--;
            if(damageCooldown <= 0){
                canDamage = true;
                damageCooldown = 150;
            }
        }
    }
}
