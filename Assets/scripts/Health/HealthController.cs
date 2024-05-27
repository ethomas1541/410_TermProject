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

    public AudioSource Audio;

    public List<AudioClip> breakClips;

    public List<AudioClip> fallClips;

    private int breakClipCount;

    private int fallClipCount;

    void Start(){
        breakClipCount = breakClips.Count;
        fallClipCount = fallClips.Count;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0){ 
            OnDeath?.Invoke();
            Audio.clip = fallClips[Random.Range(0, fallClipCount)];
        }else{
            Audio.clip = breakClips[Random.Range(0, breakClipCount)];
        }
        Audio.Play();

        OnHealthChange?.Invoke(currentHealth);
        OnTakeDamage?.Invoke();
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
