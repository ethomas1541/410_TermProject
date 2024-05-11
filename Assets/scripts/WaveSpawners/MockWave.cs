// Hunter McMahon
// 5/11/2024
// M
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MockWave : MonoBehaviour
{
    // represents an enemy prefab
    public GameObject E1;
    // holds player model, not whole prefab
    public Transform player;
    // holds camp model, not prefab as whole/    
    public Transform Camp;
    // holds main cam, in player prefab
    public Camera mainCamera;
    // needed for camp upgrade observer invokcation
    public CampUpgradeController UpgCtrl;

    public Vector3[] SpawnPoints;

    void Awake() {
        UpgCtrl.OnExitMenu += OnMenuClose;
        SpawnPoints = new Vector3[]
        {
            new Vector3(71.75f, 1f, 65.51f),
            new Vector3(45.55f, 1f, 46.44f),
            new Vector3(91.07f, 1f, 51.31f),
            new Vector3(95.14f, 1f, 50.09f),
            new Vector3(94.74f, 1f, 54.55f),
        };
    }

    void OnMenuClose()
    {
        StartCoroutine(MyCoroutine());
    }

    IEnumerator MyCoroutine()
    {
            for (int i = 0; i < 5; i++)
            {
                GameObject Enemy = Instantiate(E1, SpawnPoints[i], Quaternion.identity);
                EnemyController EC = Enemy.GetComponent<EnemyController>();
                HPBar ECam = Enemy.GetComponentInChildren<HPBar>();
                EC.Initialize(player);
                ECam.camera = mainCamera;
                yield return new WaitForSeconds(.3f);
            }

            yield return new WaitForSeconds(5.0f);

            for (int i = 0; i < 5; i++)
            {
                GameObject Enemy = Instantiate(E1, SpawnPoints[i], Quaternion.identity);
                EnemyController EC = Enemy.GetComponent<EnemyController>();
                HPBar ECam = Enemy.GetComponentInChildren<HPBar>();
                EC.Initialize(Camp);
                ECam.camera = mainCamera;
                yield return new WaitForSeconds(.3f);
            }

            // script should trigger once
            Destroy(gameObject);
    }
}
