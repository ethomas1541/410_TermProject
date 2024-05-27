using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class HealthController : MonoBehaviour
{
    // Initial values
    public int initialHealth = 100;
    public int currentHealth = 100;

    // Observer listener pattern for health change
    public delegate void HealthChanged(int currentHealth);
    public event HealthChanged OnHealthChange;

    // Observer listener pattern for taking damage
    public delegate void TakeDamge();
    public event TakeDamge OnTakeDamage;

    // Observer listener pattern for on death
    public delegate void Death();
    public event Death OnDeath;

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        OnHealthChange?.Invoke(currentHealth);
        OnTakeDamage?.Invoke();

        if (currentHealth <= 0) { OnDeath?.Invoke(); }
    }

    public void ResetHealth() {
        currentHealth = initialHealth;
    }

    public void Heal(int healAmount) {
        currentHealth += healAmount;
        if (currentHealth > initialHealth) { ResetHealth(); }
        OnHealthChange?.Invoke(currentHealth);
    }

    public void IncMaxHP(int UpgradeQ) {
        initialHealth += UpgradeQ;
        ResetHealth();
        OnHealthChange?.Invoke(currentHealth);
    }

    public float GetHP() {
        return currentHealth;
    }
}
