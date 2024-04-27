using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    // note this style of damage system only works if a rigidbody is attached to the object
    public float MaxHP = 100;

    // hitpoints
    public float HP = 100;

    [SerializeField] HPBar HealthBar;


    // Start is called before the first frame update
    public EnemyAIAnimated EnemyAgent;

    void Start()
    {
        HealthBar.UpdateHPBar(HP, MaxHP);
        // this is so we can get the death animation/function
        EnemyAgent = GetComponent<EnemyAIAnimated>();
    }

    public void TakeDamage(float DmgAmount)
    {
        HP -= DmgAmount;
        HealthBar.UpdateHPBar(HP, MaxHP);
        if (HP <= 0)
        {
            EnemyAgent.Die();
        }
    }
}
