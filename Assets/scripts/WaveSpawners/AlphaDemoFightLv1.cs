using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlphaDemoFightLv1 : MonoBehaviour
{
    // represents an enemy prefab
    public GameObject E1;
    public GameObject E2;
    public GameObject E3;

    // holds main cam, in player prefab
    public Camera mainCamera;
    //hacky solution here
    public MockWave Mwave;

    public Vector3[] SpawnPoints;


    void Awake()
    {
        SpawnPoints = new Vector3[]
        {
            new Vector3(87.2f, 1f, -28.6f),
            new Vector3(-13.48f, 1f, -35.83f),
            new Vector3(-15.53f, 1f, -39.4f),
            new Vector3(-11.83f, 1f, -39.4f),
            new Vector3(33.73f, 1f, 7.4f),
            new Vector3(21.5f, 1f, 30.65f),
            new Vector3(-16.08f, 1f, 39.33f),
            new Vector3(0f, 1f, 55f),
        };
        StartCoroutine(MyCoroutine());
    }

    IEnumerator MyCoroutine()
    {
        bool x = true;
        while (x == true)
        {
            yield return new WaitForSeconds(15f);
            for (int i = 0; i < 8; i++)
            {
                GameObject Enemy = Instantiate(E1, SpawnPoints[i], Quaternion.identity);
                Enemy EC = Enemy.GetComponent<Enemy>();
                HPBar ECam = Enemy.GetComponentInChildren<HPBar>();
                EC.SetWaveSpawner(Mwave);
                EC.SetTarget("Player");
                ECam.camera = mainCamera;
                yield return new WaitForSeconds(.2f);
            }

            yield return new WaitForSeconds(4f);

            for (int i = 0; i < 4; i++)
            {
                GameObject Enemy = Instantiate(E1, SpawnPoints[i], Quaternion.identity);
                Enemy EC = Enemy.GetComponent<Enemy>();
                HPBar ECam = Enemy.GetComponentInChildren<HPBar>();
                EC.SetWaveSpawner(Mwave);
                EC.SetTarget("Player");
                ECam.camera = mainCamera;
                yield return new WaitForSeconds(.2f);
            }

            yield return new WaitForSeconds(3f);

            for (int i = 4; i < 8; i++)
            {
                GameObject Enemy = Instantiate(E1, SpawnPoints[i], Quaternion.identity);
                Enemy EC = Enemy.GetComponent<Enemy>();
                HPBar ECam = Enemy.GetComponentInChildren<HPBar>();
                EC.SetWaveSpawner(Mwave);
                EC.SetTarget("Camp");
                ECam.camera = mainCamera;
                yield return new WaitForSeconds(.2f);
            }



            yield return new WaitForSeconds(15f);
        }
    }
}
