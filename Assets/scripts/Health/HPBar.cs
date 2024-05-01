// made by following https://www.youtube.com/watch?v=_lREXfAMUcE&t=4s&ab_channel=BMo
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    public HealthController healthController;
    public Slider slider;
    public Camera Camera;

    public void Initialize(Camera c)
    {
        Camera = c;
    }

    void Start() {

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

    void Update()
    {
        transform.rotation = Camera.transform.rotation;
    }
}
