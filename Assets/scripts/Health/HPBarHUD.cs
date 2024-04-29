using UnityEngine;
using UnityEngine.UI;

public class HPBarHUD : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;

    public void UpdateHPBar(float currentHP, float maxHP) {
        slider.value = currentHP / maxHP;
    }
}
