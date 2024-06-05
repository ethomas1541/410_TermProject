using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class WaveEnemy
{
    public GameObject enemy;
    public int count;
    public float spawnChance;
}

public enum SpawnState { COUNTING, WAITING, SPAWNING }

public class WaveSpawner : MonoBehaviour
{
    public WaveEnemy[] enemies;
    public GameObject finalBoss;
    public float secondsBetweenWaves = 5;
    public float secondsDelta = 1;
    public int spawnCount = 3;
    public int spawnDelta = 2;
    public float spawnRate = 3.5f;
    public int waveCount = 5;

    // Keep these public for debug purposes
    public Transform[] spawnPoints;
    public string[] targets = { "Player", "Camp" };
    public SpawnState currentState = SpawnState.WAITING;
    public int currentWave = 1;

    private float enemyCheckFrequency = 1;
    private System.Random random = new System.Random();

    // Wave timer
    public delegate void TimerChange(float timeLeft);
    public event TimerChange OnTimeChange;

    // Wave increment
    public delegate void WavesChange(int wave);
    public event WavesChange OnWavesChange;

    // Waves completed
    public delegate void WavesCompltes();
    public event WavesCompltes OnWavesCompleted;

    // win menu
    public GameObject WinMenu;

    void Start()
    {
        // Get all spawn points excluding this (only children of wave spawner can be spawn points)
        spawnPoints = GetComponentsInChildren<Transform>().Skip(1).ToArray();
        StartCoroutine(Waiting());
    }

    private IEnumerator Counting()
    {
        Debug.Log("Counting");

        // Check once per second if the enemies are alive
        while (IsEnemyAlive())
        {
            yield return new WaitForSeconds(enemyCheckFrequency);
        }

        // End of state
        currentState = SpawnState.WAITING;
        StartCoroutine(Waiting());
    }

    private IEnumerator Spawning()
    {
        Debug.Log("Spawning");

        currentWave += 1;
        OnWavesChange?.Invoke(currentWave);

        // DO SPAWNING LOGIC HERE
        for (int i = 0; i < spawnCount; i++)
        {
            WaveEnemy waveEnemy = SelectRandomWaveEnemy();
            SpawnWaveEnemy(waveEnemy);
            yield return new WaitForSeconds(spawnRate);
        }

        spawnCount += spawnDelta;

        // End of state
        currentState = SpawnState.COUNTING;
        StartCoroutine(Counting());
    }

    private IEnumerator Waiting()
    {
        float countdown = secondsBetweenWaves;

        // Wait between waves then update wait time
        while (countdown > 0)
        {
            yield return new WaitForSeconds(1f);
            countdown -= 1f;
            OnTimeChange?.Invoke(countdown);
        }

        UpdateSecondsBetweenWaves();

        // End of state
        if (currentWave < waveCount)
        {
            currentState = SpawnState.SPAWNING;
            StartCoroutine(Spawning());
        }
        else
        {
            // We are done
            OnWavesCompleted?.Invoke();
            Transform spawnPoint = spawnPoints[random.Next(0, spawnPoints.Length)];
            GameObject enemy = Instantiate(finalBoss, spawnPoint.position, spawnPoint.rotation);
            if (enemy.GetComponent<Enemy>() != null)
            {
                enemy.GetComponent<Enemy>().targetTag = "Player";
            }
            enemy.GetComponent<HealthController>().OnDeath += OnWin;
        }
    }

    // on boss fight win
    public void OnWin()
    {
        Time.timeScale = 0f;
        WinMenu.SetActive(true);
    }

    private void UpdateSecondsBetweenWaves()
    {
        // Keep the seconds between waves above 0
        secondsBetweenWaves = (secondsBetweenWaves + secondsDelta > 0) ? secondsBetweenWaves + secondsDelta : secondsBetweenWaves;
    }

    private WaveEnemy SelectRandomWaveEnemy()
    {
        WaveEnemy selectedEnemy = null;

        float totalWeight = enemies.Sum(e => e.spawnChance);
        float randomValue = (float)random.NextDouble() * totalWeight;

        foreach (WaveEnemy enemy in enemies)
        {
            if (randomValue < enemy.spawnChance)
            {
                selectedEnemy = enemy;
                break;
            }

            randomValue -= enemy.spawnChance;
        }

        // If we didn't select an enemy then just select the first enemy
        selectedEnemy = (selectedEnemy == null) ? enemies[0] : selectedEnemy;

        return selectedEnemy;
    }

    private void SpawnWaveEnemy(WaveEnemy waveEnemy)
    {
        for (int i = 0; i < waveEnemy.count; i++)
        {
            Transform spawnPoint = spawnPoints[random.Next(0, spawnPoints.Length)];
            Enemy enemy = Instantiate(waveEnemy.enemy, spawnPoint.position, spawnPoint.rotation).GetComponent<Enemy>();
            enemy.targetTag = targets[random.Next(0, targets.Length)];
        }
    }

    private bool IsEnemyAlive()
    {
        // We should limit the number of times we do this per second
        return GameObject.FindGameObjectWithTag("Enemy") != null;
    }
}
