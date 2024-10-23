using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScript : MonoBehaviour
{
    public int spawnFreqMax = 20;
    public int spawnFreqMin = 8;
    public GameObject enemyPrefab;
    public int maxEnemies = 3;
    private int waveTimer = 0;
    private int spawnFreq = 0;
    private int waveNumber = 1;
    void Start()
    {
        StartCoroutine(waveMethod());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator spawnEnemyOnTimer()
    {
        while (true)
        {
            int enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
            if (enemyCount < maxEnemies)
            {
                Debug.Log("Enemy spawned");
                Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            }
            else
            {
                Debug.Log("Max enemies reached");
            }

            spawnFreq = Random.Range(spawnFreqMin, spawnFreqMax);
            yield return new WaitForSecondsRealtime(spawnFreq);
        }
    }

    IEnumerator waveMethod()
    {
        while (true)
        {
            if (waveNumber == 1)
            {
                waveTimer = 30;
                maxEnemies = 4;
                spawnFreqMax = 10;
                spawnFreqMin = 8;
                StartCoroutine(spawnEnemyOnTimer());
                yield return new WaitForSecondsRealtime(waveTimer);
            }

            waveNumber++;
            waveTimer = 25; 
            maxEnemies += 2; 
            spawnFreqMin -= 2;
            if (spawnFreqMin < 3)
            {
                spawnFreqMin = 3;
            }

            Debug.Log("Wave " + waveNumber + " starting in " + waveTimer + " seconds.");
            yield return new WaitForSecondsRealtime(waveTimer);
        }
    }
}