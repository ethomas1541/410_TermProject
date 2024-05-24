/*
https://www.youtube.com/watch?v=Vrld13ypX_I&t=0s (part 1)
https://www.youtube.com/watch?v=q0SBfDFn2Bs&t=607s (part 2)
*/

using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class WaveSpawnerOld : MonoBehaviour
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

    // for da wave
    public Wave[] waves;
    private int next_wave = 0;

    // for enemies
    public Transform[] spawn_points;

    // time variables for da wave
    public float time_between_waves = 5f;
    private float wave_countdown;

    // time between checking if enemeis are alive
    private float search_countdown = 1f;

    // holds current state of wave
    public SpawnState state = SpawnState.COUNTING;

    void Start()
    {
        wave_countdown = time_between_waves;

        // stores current state of spawner

    }

    void Update()
    {
        if (state == SpawnState.WAITING)
        {
            // check if enemies still alive during wave
            if (!EnemyIsAlive())
            {
                // start new wave
                WaveCompleted();
            }
            else
            {
                // enemies still alive so stop here
                return;
            }
        }

        // start spawning waves
        if (wave_countdown <= 0)
        {
            // make sure waves are not already spawning
            if (state != SpawnState.SPAWNING)
            {
                // calls the IEnumerator SpawnWave method to start spawning
                StartCoroutine(SpawnWave(waves[next_wave]));
            }
        }
        else
        {
            wave_countdown -= Time.deltaTime;
        }
    }

    void WaveCompleted()
    {
        Debug.Log("wave complete");

        // start counting down again
        state = SpawnState.COUNTING;

        wave_countdown = time_between_waves;

        // check if index of next wave is > number of waves
        if (next_wave + 1 > waves.Length - 1)
        {
            next_wave = 0;

            // from here can edit waves (stat multiplier, game finish screen, start new scene, etc.)
            Debug.Log("completed all waves, looping...");
        }
        else
        {
            // continue to next wave
            next_wave++;
        }

    }

    // check if enemy alive or 💀
    bool EnemyIsAlive()
    {
        search_countdown -= Time.deltaTime;
        if (search_countdown <= 0f)
        {
            // enemies are still alive, check again in one second
            search_countdown = 1f;

            if (GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                return false;
            }
        }
        return true;
    }

    // wave nursery (method will wait a certain amount of seconds before continuing)
    IEnumerator SpawnWave(Wave _wave)
    {
        Debug.Log("Spawning wave: " + _wave.name);

        // fight begins
        state = SpawnState.SPAWNING;

        // spawn
        for (int i = 0; i < _wave.count; i++)
        {
            SpawnEnemy(_wave.enemy);

            // wait some time before spawning another enemy
            yield return new WaitForSeconds(1f / _wave.spawn_rate);
        }

        // wait for player to finish the wave
        state = SpawnState.WAITING;

        // stops IEnumerator from expecting a return value
        yield break;
    }

    // where enemies are born
    void SpawnEnemy(Transform _enemy)
    {
        Debug.Log("Spawning: " + _enemy.name);

        // choose random spawn point
        Transform _sp = spawn_points[Random.Range(0, spawn_points.Length)];

        // instantiate enemy at Game Manager's position (0,0,0)
        Transform e = Instantiate(_enemy, _sp.position, _sp.rotation);

        // Make this game object active
        e.gameObject.SetActive(true);
    }
}
