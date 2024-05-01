using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    // Initial values
    public int initialHealth = 100;
    public int currentHealth = 100;

    // Observer listener pattern, not all objects need a health bar
    public delegate void HealthChanged(int currentHealth);
    public event HealthChanged OnHealthChange;

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        OnHealthChange?.Invoke(currentHealth);
    }

    public void ResetHealth() {
        currentHealth = initialHealth;
    }

    public void Heal(int healAmount) {
        currentHealth += healAmount;
        if (currentHealth > initialHealth) { ResetHealth(); }
        OnHealthChange?.Invoke(currentHealth);
    }

    public float GetHP() {
        return currentHealth;
    }
}
