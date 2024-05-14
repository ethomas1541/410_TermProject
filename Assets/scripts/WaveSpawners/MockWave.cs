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

    public string playerTag;
    public Transform campTag;

    // holds main cam, in player prefab
    public Camera mainCamera;
    // needed for camp upgrade observer invokcation
    public CampUpgradeController UpgCtrl;
    public Vector3[] SpawnPoints;
    private bool isActive = true;

    // Win condition tracker
    public GameObject WinMenu;
    public int Enemies_Slain = 0;
    public int WinAmount = 11;

    void Awake() {
        WinMenu.SetActive(false);
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
        if(isActive)
        {
            StartCoroutine(MyCoroutine());
            isActive = false;
        }
    }

    IEnumerator MyCoroutine()
    {
            for (int i = 0; i < 5; i++)
            {
                GameObject Enemy = Instantiate(E1, SpawnPoints[i], Quaternion.identity);
                Enemy EC = Enemy.GetComponent<Enemy>();
                HPBar ECam = Enemy.GetComponentInChildren<HPBar>();
                EC.SetWaveSpawner(this);
                EC.SetTarget("Player");
                ECam.camera = mainCamera;
                yield return new WaitForSeconds(.3f);
            }

            yield return new WaitForSeconds(5.0f);

            for (int i = 0; i < 5; i++)
            {
                GameObject Enemy = Instantiate(E1, SpawnPoints[i], Quaternion.identity);
                Enemy EC = Enemy.GetComponent<Enemy>();
                HPBar ECam = Enemy.GetComponentInChildren<HPBar>();
                EC.SetWaveSpawner(this);
                EC.SetTarget("Camp");
                ECam.camera = mainCamera;
                yield return new WaitForSeconds(.3f);
            }
    }

    public void Enemykilled()
    {
        Enemies_Slain ++;
        if (Enemies_Slain == WinAmount)
        {
            // bring up the win screen
            Time.timeScale = 0f;
            WinMenu.SetActive(true);
        }
    }
}
