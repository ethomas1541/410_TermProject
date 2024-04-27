using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    // define enum to track wave state
    public enum SpawnState { SPAWNING, WAITING, COUNTING };

    // can change values of a wave instance inside inspector
    [System.Serializable]

    // define what a wave should be
    public class Wave
    {
        public string name;
        public Transform enemy;
        public int count;
        public float spawn_rate;
    }

    public Wave[] waves;

    // for da wave
    private int next_wave = 0;

    public float time_between_waves = 5f;
    public float wave_countdown;

    private SpawnState state = SpawnState.COUNTING;

    void Start()
    {
        wave_countdown = time_between_waves;

        // stores current state of spawner

    }

    void Update()
    {
        // start spawning waves
        if (wave_countdown <= 0)
        {
            // make sure waves are not already spawning
            if (state != SpawnState.SPAWNING)
            {
                // calls the IEnumerator SpawnWave method to start spawning 
                StartCoroutine(SpawnWave (waves[next_wave]));
            }
            else
            {
                wave_countdown -= Time.deltaTime;
            }
        }
    }

    // wave nursery (method will wait a certain amount of seconds before continuing)
    IEnumerator SpawnWave(Wave _wave)
    {
        // fight begins
        state = SpawnState.SPAWNING;

        // spawn 
        for (int i = 0; i < _wave.count; i++)
        {
            SpawnEnemy(_wave.enemy);

            // wait some time before spawning another enemy
            yield return new WaitForSeconds(1f/_wave.spawn_rate);
        }

        // wait for player to finish the wave
        state = SpawnState.WAITING;

        // stops IEnumerator from expecting a return value
        yield break;
    }

    // where enemies are born
    void SpawnEnemy (Transform _enemy)
    {
        Debug.Log("Spawning: " + _enemy.name);
    }
}
