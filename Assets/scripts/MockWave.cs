using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MockWave : MonoBehaviour
{
    // represents an enemy prefab
    public GameObject E1;
    // holds player model, not whole prefab
    public Transform player;
    // holds camp model, not prefab as whole
    public Transform Camp;
    // holds main cam, in player prefab
    public Camera mainCamera;

    private bool Active = true;
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && Active == true)
        {   
            int n = 54;
            for( int i = 0; i < 6; i++)
            {
                GameObject Enemy = Instantiate(E1, new Vector3(56, 1, n), Quaternion.identity);
                EnemyController EC = Enemy.GetComponent<EnemyController>();
                HPBar ECam = Enemy.GetComponentInChildren<HPBar>();
                EC.Initialize(player);
                ECam.Initialize(mainCamera);
                n += 2;
            }

            for (int i = 0; i < 4; i++)
            {
                GameObject Enemy = Instantiate(E1, new Vector3(91, 1, n), Quaternion.identity);
                EnemyController EC = Enemy.GetComponent<EnemyController>();
                HPBar ECam = Enemy.GetComponentInChildren<HPBar>();
                EC.Initialize(Camp);
                ECam.Initialize(mainCamera);
                n += 2;
            }
            Active = false;
        }
    }
}
