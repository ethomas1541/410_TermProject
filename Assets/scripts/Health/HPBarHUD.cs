using UnityEngine;
using UnityEngine.UI;

public class HPBarHUD : MonoBehaviour
{
    public HealthController healthController;
    public Slider slider;

    void Start()
    {
        // Subscribe to health controller notificiations
        healthController.OnHealthChange += UpdateHPBar;

        // Update inital values
        slider.maxValue = healthController.initialHealth;
        slider.value = healthController.currentHealth;
    }

    void OnDisable()
    {
        // Unsubscribe from health controller notifications
        healthController.OnHealthChange -= UpdateHPBar;
    }

    public void UpdateHPBar(int currentHealth)
    {
        slider.value = currentHealth;
    }
}
