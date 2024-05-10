using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstFight : MonoBehaviour
{
    public GameObject E1;
    public Transform player;
    public Camera mainCamera;

    private bool NotTriggered = true;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && NotTriggered)
        {
            GameObject Enemy = Instantiate(E1, new Vector3(84, 1, 18), Quaternion.identity);
            EnemyController EC = Enemy.GetComponent<EnemyController>();
            HPBar ECam = Enemy.GetComponentInChildren<HPBar>();
            EC.target = player;
            ECam.camera = mainCamera;
            NotTriggered = false;
        }
    }
}
