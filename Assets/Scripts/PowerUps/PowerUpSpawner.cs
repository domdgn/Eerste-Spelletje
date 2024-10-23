using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    public GameObject healthKitPrefab;
    public GameObject rapidFirePrefab;
    private Vector2 xBounds = new Vector2(-15, 15);
    private Vector2 zBounds = new Vector2(-10, 10);
    private float yPosition = 1f;
    private Transform player;

    void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogWarning("Player object not found!");
            return;
        }
        StartCoroutine(SpawnHealthKit());
        StartCoroutine(SpawnRapidFire());
    }

    IEnumerator SpawnHealthKit()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(5.0f, 7.5f));
            float randomX = Random.Range(xBounds.x, xBounds.y);
            float randomZ = Random.Range(zBounds.x, zBounds.y);
            Vector3 spawnPosition = new Vector3(randomX, yPosition, randomZ) + player.position;
            Instantiate(healthKitPrefab, spawnPosition, Quaternion.identity);
        }
    }

    IEnumerator SpawnRapidFire()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(5.5f, 9.5f));
            float randomX = Random.Range(xBounds.x, xBounds.y);
            float randomZ = Random.Range(zBounds.x, zBounds.y);
            Vector3 spawnPosition = new Vector3(randomX, yPosition, randomZ) + player.position;
            Instantiate(rapidFirePrefab, spawnPosition, Quaternion.identity);
        }
    }
}
