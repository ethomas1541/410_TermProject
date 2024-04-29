using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public float max_hp = 100;
    public float hp = 100;
    public HPBarHUD health_bar;

    void Start()
    {
        health_bar.UpdateHPBar(hp, max_hp);
    }

    public void TakeDamage(float damage)
    {
        hp -= damage;
        health_bar.UpdateHPBar(hp, max_hp);

        if (hp <= 0.0f) {
            Debug.Log("Game is Over");
        }
    }
}
