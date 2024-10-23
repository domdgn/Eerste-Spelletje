using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthKitSpawner : MonoBehaviour
{
    public GameObject healthKitPrefab;
    public Vector2 xBounds = new Vector2(-12, 12);
    public Vector2 zBounds = new Vector2(-7, 7);
    public float yPosition = 1f;
    private int kitCount = 0;

    void Start()
    {
        StartCoroutine(SpawnHealthKit());
    }

    private void Update()
    {
        kitCount = GameObject.FindGameObjectsWithTag("HealthKit").Length;
    }

    IEnumerator SpawnHealthKit()
    {
        while (kitCount < 3)
        {
            yield return new WaitForSeconds(Random.Range(10.0f, 30.0f));

            float randomX = Random.Range(xBounds.x, xBounds.y);
            float randomZ = Random.Range(zBounds.x, zBounds.y);
            Vector3 spawnPosition = new Vector3(randomX, yPosition, randomZ);

            Instantiate(healthKitPrefab, spawnPosition, Quaternion.identity);
        }
    }
}
