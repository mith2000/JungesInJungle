using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public Enemy[] enemies;
        public int countEnemiesInWave;
        public float delayBetweenSpawns;
    }

    public Wave[] waves;
    public Transform[] spawnPoints;
    public float delayBetweenWaves;

    [HideInInspector] public Wave currentWave;
    [HideInInspector] public int currentWaveIndex;
    [HideInInspector] public bool finishedSpawn;

    [HideInInspector] public bool finishedWave = false;

    void Start()
    {
        StartCoroutine(StartNextWave(currentWaveIndex));
    }

    IEnumerator StartNextWave(int index)
    {
        yield return new WaitForSeconds(delayBetweenWaves);
        StartCoroutine(SpawnWave(index));
    }

    IEnumerator SpawnWave(int index)
    {
        currentWave = waves[index];

        for (int i = 0; i < currentWave.countEnemiesInWave; i++)
        {
            Enemy randomEnemy = currentWave.enemies[Random.Range(0, currentWave.enemies.Length)];
            Transform randomSpot = spawnPoints[Random.Range(0, spawnPoints.Length)];
            Instantiate(randomEnemy, randomSpot.position, randomSpot.rotation);

            if (i == currentWave.countEnemiesInWave - 1)
            {
                finishedSpawn = true;
            }
            else
            {
                finishedSpawn = false;
            }

            yield return new WaitForSeconds(currentWave.delayBetweenSpawns);
        }
    }

    private void Update()
    {
        if (finishedSpawn && GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            finishedSpawn = false;
            if (currentWaveIndex + 1 < waves.Length)
            {
                currentWaveIndex++;
                StartCoroutine(StartNextWave(currentWaveIndex));
            }
            else
            {
                finishedWave = true;
            }
        }
    }
}
